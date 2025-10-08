using System.Reflection.PortableExecutable;

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

    public List<int> CalculatedPrimeNumbers()
    {
        
        return _primes;
    }
}
