using System;
using System.Collections.Generic;
using System.Linq;
using NUnit;

public class TaskSchedulerTests
{

    private static DateTime Now => DateTime.UtcNow;

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Schedule_AddsTasksCorrectly()
    {
        var scheduler = new TaskScheduler();
        var task = new TaskItem("T1", "Test", PriorityLevel.Medium, Now.AddHours(1));

        scheduler.Schedule(task);

        Assert.Equal(task, scheduler.PeekNext());
    }

    [Test]
    public void ExecuteNext_ReturnsHighestPriorityFirst()
    {
        var scheduler = new TaskScheduler();

        var low = new TaskItem("L1", "Low", PriorityLevel.Low, Now.AddMinutes(10));
        var med = new TaskItem("M1", "Med", PriorityLevel.Medium, Now.AddMinutes(5));
        var high = new TaskItem("H1", "High", PriorityLevel.High, Now.AddMinutes(15));

        scheduler.Schedule(low);
        scheduler.Schedule(med);
        scheduler.Schedule(high);

        Assert.Equal(high, scheduler.ExecuteNext());
        Assert.Equal(med, scheduler.PeekNext()); // Next should be Medium
    }

    [Test]
    public void ExecuteNext_SamePriority_EarliestDeadlineFirst()
    {
        var scheduler = new TaskScheduler();

        var h1 = new TaskItem("H1", "Later", PriorityLevel.High, Now.AddHours(2));
        var h2 = new TaskItem("H2", "Sooner", PriorityLevel.High, Now.AddMinutes(30));

        scheduler.Schedule(h1);
        scheduler.Schedule(h2);

        Assert.Equal(h2, scheduler.ExecuteNext()); // h2 has earlier deadline
        Assert.Equal(h1, scheduler.ExecuteNext());
    }

    [Test]
    public void PeekNext_DoesNotRemoveTask()
    {
        var scheduler = new TaskScheduler();
        var task = new TaskItem("T1", "Test", PriorityLevel.High, Now.AddHours(1));
        scheduler.Schedule(task);

        Assert.Equal(task, scheduler.PeekNext());
        Assert.Equal(task, scheduler.PeekNext()); // Still there
        Assert.Equal(task, scheduler.ExecuteNext()); // Now removed
        Assert.Throws<InvalidOperationException>(() => scheduler.PeekNext());
    }

    [Test]
    public void ExecuteNext_EmptyQueue_ThrowsInvalidOperationException()
    {
        var scheduler = new TaskScheduler();
        Assert.Throws<InvalidOperationException>(() => scheduler.ExecuteNext());
    }

    [Test]
    public void Schedule_NullTask_ThrowsArgumentNullException()
    {
        var scheduler = new TaskScheduler();
        Assert.Throws<ArgumentNullException>(() => scheduler.Schedule(null!));
    }

    [Test]
    public void GetOverdueTasks_ReturnsOnlyOverdue()
    {
        var scheduler = new TaskScheduler();

        var past1 = new TaskItem("P1", "Past 1", PriorityLevel.Low, Now.AddHours(-2));
        var past2 = new TaskItem("P2", "Past 2", PriorityLevel.High, Now.AddHours(-1));
        var future = new TaskItem("F1", "Future", PriorityLevel.Medium, Now.AddHours(1));

        scheduler.Schedule(past1);
        scheduler.Schedule(past2);
        scheduler.Schedule(future);

        var overdue = scheduler.GetOverdueTasks().ToList();

        Assert.Contains(past1, overdue);
        Assert.Contains(past2, overdue);
        Assert.DoesNotContain(future, overdue);
        Assert.Equal(2, overdue.Count);
    }

    [Test]
    public void Schedule_DuplicateId_OverwritesOrThrows()
    {
        var scheduler = new TaskScheduler();

        var t1 = new TaskItem("T1", "Old", PriorityLevel.Low, Now.AddHours(1));
        var t2 = new TaskItem("T1", "New", PriorityLevel.High, Now.AddMinutes(30));

        scheduler.Schedule(t1);
        scheduler.Schedule(t2);

        // Option 1: Überprüfe, ob überschrieben wurde
        var next = scheduler.PeekNext();
        Assert.Equal("New", next.Description);
        Assert.Equal(PriorityLevel.High, next.Priority);

        // Option 2: Alternativ: Exception werfen → dann diesen Test anpassen
        // Assert.Throws<InvalidOperationException>(() => scheduler.Schedule(t2));
    }

    [Test]
    public void Performance_IsEfficient_LargeNumberOfTasks()
    {
        var scheduler = new TaskScheduler();
        var random = new Random(42);

        // Füge 10.000 Tasks ein
        for (int i = 0; i < 10_000; i++)
        {
            var priority = (PriorityLevel)random.Next(1, 4);
            var deadline = Now.AddMinutes(random.Next(-100, 100));
            scheduler.Schedule(new TaskItem($"T{i}", $"Task {i}", priority, deadline));
        }

        // Sollte schnell sein (O(n log n))
        var start = DateTime.UtcNow;
        var next = scheduler.ExecuteNext();
        var duration = DateTime.UtcNow - start;

        Assert.True(duration.TotalMilliseconds < 100, $"Execution took {duration.TotalMilliseconds}ms – too slow!");
    }
}