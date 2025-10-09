using Microsoft.VisualBasic;

namespace ThreadingImpl;

public class TasksExample
{
    //TODO: Download List of URLs in Parallel
    public async Task Run_Download_Parallel(List<string> URLs, int Parallelisation)
    {
        //Begerenzt maximale Ausführung von Tasks => ansonsten für jede URL eigene Task => zu viel
        var results = new List<(string Url, int ContentLength)>();
        //Task creation
        var tasks = URLs.Select(x => DownloadUrl(x, results, Parallelisation));
        // Ergebnisse sortiert ausgeben
        await Task.WhenAll(tasks);
        Console.WriteLine("\nErgebnisse (sortiert nach Content-Length):");
        var sortedResults = results.OrderByDescending(r => r.ContentLength).ToList();
        foreach (var result in sortedResults)
        {
            Console.WriteLine($"URL: {result.Url,-50} Content-Length: {result.ContentLength}");
        }
    }

    private async Task DownloadUrl(string url, List<(string Url, int ContentLength)> result, int Parallelisation)
    {
        //Enter thread safety
        using var semaphore = new SemaphoreSlim(Parallelisation);
        try
        {
            await semaphore.WaitAsync();
            using var client = new HttpClient { Timeout = TimeSpan.FromSeconds(3) };
            var content = await client.GetByteArrayAsync(url);
            //Protected result list via lock notwendig da List nicht thread sicher
            lock (result)
            {
                result.Add((url, content.Length));
            }
            Console.WriteLine($"Download von {url} auf Thread {Thread.CurrentThread.ManagedThreadId}");

        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Fehler bei {url}: {ex.Message}");
            lock (result)
            {
                result.Add((url, 0));
            }
        }
        finally
        {
            semaphore.Release();
        }
    }

    public void EasyTaskEntrance()
    {
        Task task = new Task(() =>
        {
            Console.WriteLine("Task entered");
            int result = AddNumbers(5, 10);
            Console.WriteLine($"Result = {result}");
        });

        task.Start();
        Console.WriteLine("Main Thread is done");
    }
    
    private int AddNumbers(int a, int b)
    {
        return a + b;
    }
}