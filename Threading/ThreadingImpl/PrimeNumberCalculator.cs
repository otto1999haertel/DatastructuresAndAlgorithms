using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;

namespace ThreadingImpl;
//TODO: calculate prime numbers in giving range (e.g. 1 to 100.000) in parallel => 4 Abschnitte
//Collect results in thread save list via lock
//Implement calculation token
public class PrimeNumberCalculator
{
    private int _min;
    private int _max;
    private int _sections;
    private List<int> _primes = new List<int>();
    private readonly object _lockedObject = new object();

    public PrimeNumberCalculator(int Min, int Max, int Sections)
    {
        _min = Min;
        _max = Max;
        _sections = Sections;
    }

    public async Task<List<int>> CalculatedPrimeNumbers(CancellationToken cancellationToken)
    {
        int increment = (_max - _min) / _sections;
        var tasks = new List<Task<List<int>>>();
        for (int i = 0; i < _sections; i++)
        {
            int rangeStart = _min + i * increment;
            int rangeEnd = i == _sections - 1 ? _max : _min + (i + 1) * increment;
            //Vordefinieren der Tasks für spätere parellele Ausführung
            tasks.Add(CalculationTask(rangeStart, rangeEnd, cancellationToken));
 
        }
        var result  = await Task.WhenAll(tasks); //Ausführung der Tasks und jedes Abschnittes parellel
        foreach (var primeList in result)
            {
                lock (_lockedObject) // _lockObject als private readonly object definieren
                {
                    _primes.AddRange(primeList);
                }
            }
        return _primes;
    }

    private Task<List<int>> CalculationTask(int min, int max, CancellationToken cancellationToken)
    {
        //Zuweisung der Task zu einem verfügbaren Thread mit
       return Task.Run(() =>
             {
                 List<int> primeNumberResult = new List<int>();
                 if (_sections == 1)
                 {
                     max += 1;
                 }
                 for (int i = min; i < max; i++)
                 {
                     cancellationToken.ThrowIfCancellationRequested();
                     bool primNumber = true;
                     for (int j = 2; j <= (int)Math.Sqrt(i); j++)
                     {
                         if (i % j == 0)
                         {
                             primNumber = false;
                             break;
                         }
                     }
                     if (!primeNumberResult.Contains(i) && primNumber && i != 1)
                     {
                         primeNumberResult.Add(i);
                         Console.WriteLine($"Primzahl {i} gefunden in Bereich {min}-{max} auf Thread {Thread.CurrentThread.ManagedThreadId}");
                     }
                 }
                 Console.WriteLine($"Task für Bereich {min}-{max} beendet auf Thread {Thread.CurrentThread.ManagedThreadId}");
                 return primeNumberResult;
             }, cancellationToken);
    }
}
