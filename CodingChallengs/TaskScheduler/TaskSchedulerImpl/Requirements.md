# Mid-Level C# Coding Challenge: Smart Task Scheduler

> **Level:** Mid-Level Software Engineer (C#)  
> **Dauer:** 45–60 Minuten (inkl. Diskussion)  
> **Framework:** .NET 6 oder höher  
> **Ziel:** Implementiere einen intelligenten Task-Scheduler mit Priorität und Deadline – wie ein Mini-Job-Queue-System.

---

## Aufgabenstellung

Implementiere die Klasse `TaskScheduler` mit **genau diesen öffentlichen Methoden**:

```csharp
public class TaskScheduler
{
    /// <summary>
    /// Fügt eine Aufgabe hinzu. Bei doppelter Id: überschreibe oder wirf Exception.
    /// </summary>
    public void Schedule(TaskItem task) { }

    /// <summary>
    /// Gibt die nächste auszuführende Aufgabe zurück – OHNE zu entfernen.
    /// </summary>
    public TaskItem PeekNext() { }

    /// <summary>
    /// Entfernt und gibt die nächste auszuführende Aufgabe zurück.
    /// </summary>
    public TaskItem ExecuteNext() { }

    /// <summary>
    /// Gibt alle Tasks zurück, deren Deadline überschritten ist (Deadline < DateTime.Now).
    /// Reihenfolge beliebig.
    /// </summary>
    public IEnumerable<TaskItem> GetOverdueTasks() { }
}
}

public class TaskItem
{
    public string Id { get; }
    public string Description { get; }
    public PriorityLevel Priority { get; }
    public DateTime Deadline { get; }

    public TaskItem(string id, string description, PriorityLevel priority, DateTime deadline)
    {
        Id = id ?? throw new ArgumentNullException(nameof(id));
        Description = description ?? throw new ArgumentNullException(nameof(description));
        Priority = priority;
        Deadline = deadline;
    }
}

public enum PriorityLevel
{
    Low = 1,
    Medium = 2,
    High = 3
}

var scheduler = new TaskScheduler();

scheduler.Schedule(new TaskItem("T1", "Datenbank sichern", PriorityLevel.High, DateTime.Now.AddHours(1)));
scheduler.Schedule(new TaskItem("T2", "E-Mail senden", PriorityLevel.Medium, DateTime.Now.AddMinutes(30)));
scheduler.Schedule(new TaskItem("T3", "Cache leeren", PriorityLevel.High, DateTime.Now.AddHours(2)));

var next = scheduler.ExecuteNext();
Console.WriteLine(next.Description); // Ausgabe: "Datenbank sichern" (T1) 
```

Test