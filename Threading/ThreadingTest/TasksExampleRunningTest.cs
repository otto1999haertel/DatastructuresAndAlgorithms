namespace ThreadingTest;

using System.Diagnostics;
using System.Threading.Tasks;
using ThreadingImpl;
public class TasksExampleRunningTest
{
    [Test]
    [TestCase(1)]
    [TestCase(2)]
    [TestCase(4)]
    public async Task TasksExample_PerformanceTest(int parallelisation)
    {
        var tasksExample = new TasksExample();
        var urls = new List<string>
        {
            // Ursprüngliche URLs (geprüft auf Zuverlässigkeit)
            "https://www.example.com",
            "https://www.github.com",
            "https://www.microsoft.com",
            "https://www.bbc.com",
            "https://www.mozilla.org",
            "https://www.python.org",
            "https://www.otto.de",
            "https://www.nasa.gov",
            "https://www.theguardian.com",
            "https://www.apache.org",
            "https://www.cloudflare.com",
            // Zusätzliche URLs für höhere Testlast
            "https://www.nytimes.com",
            "https://www.amazon.com",
            "https://www.harvard.edu",
            "https://www.nationalgeographic.com",
            "https://www.bbc.co.uk",
            "https://www.cnn.com",
            "https://www.forbes.com",
            "https://www.reuters.com",
            "https://www.bloomberg.com",
            "https://www.ibm.com",
            "https://www.oracle.com",
            "https://www.salesforce.com",
            "https://www.dell.com",
            "https://www.hp.com",
            "https://www.cisco.com",
            "https://www.netflix.com",
            "https://www.spotify.com",
            "https://www.dropbox.com",
            "https://www.atlassian.com"
        };
        // Zeitmessung starten
        var stopwatch = Stopwatch.StartNew();
        await tasksExample.Run_Download_Parallel(urls, parallelisation);
        stopwatch.Stop();
        double avgTimePerUrl = stopwatch.ElapsedMilliseconds / (double)urls.Count;

        // Ergebnisse ausgeben
        Console.WriteLine($"Parallelisation: {parallelisation}");
        Console.WriteLine($"Gesamtdauer: {stopwatch.ElapsedMilliseconds} ms");
        Console.WriteLine($"Durchschnittliche Zeit pro URL: {avgTimePerUrl:F2} ms");
    }
    [Test]
    public void EasyTaskExec_Test()
    {
        var tasksExample = new TasksExample();
        tasksExample.EasyTaskEntrance();
    } 
}