namespace ThreadingTest;
using ThreadingImpl;
public class TasksExampleRunningTest
{
    [Test]
    public void TasksExample_Test()
    {
        TasksExample tasksExample = new TasksExample();
        tasksExample.Run();
    }
}