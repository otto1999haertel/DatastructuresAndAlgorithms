namespace ThreadingTest;
using ThreadingImpl;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        PrimeNumberCalculator primeNumberCalculator = new PrimeNumberCalculator(1, 20, 2);
        primeNumberCalculator.CalculatedPrimeNumbers(new CancellationToken());
    }
}
