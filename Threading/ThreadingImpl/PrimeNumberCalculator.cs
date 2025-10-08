using System.Reflection.PortableExecutable;

namespace ThreadingImpl;
//TODO: calculate prime numbers in giving range (e.g. 1 to 100.000) in parallel => 4 Abschnitte
//Collect results in thread save list via lock
//Implement calculation token
public class PrimeNumberCalculator
{
    private int _min = 0;
    private int _max = 0;
    private int _sections = 0;

    public PrimeNumberCalculator(int Min, int Max, int Sections)
    {
        _min = Min;
        _max = Max;
        _sections = Sections;
    }
}
