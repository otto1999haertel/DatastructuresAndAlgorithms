namespace ThreadingTest;
using ThreadingImpl;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    [TestCase(1, 10, 2, new int[] { 2, 3, 5, 7 })]
    [TestCase(1, 10, 2, new int[] { 2, 3, 5, 7 })]
    [TestCase(1, 20, 4, new int[] { 2, 3, 5, 7, 11, 13, 17, 19 })]
    [TestCase(10, 20, 2, new int[] { 11, 13, 17, 19 })]
    [TestCase(1, 5, 1, new int[] { 2, 3, 5 })]
    [TestCase(21, 30, 3, new int[] { 23, 29 })]
    [TestCase(100, 100, 2, new int[] { })]
    [TestCase(2, 2, 1, new int[] { 2 })]
    public void CalculatedPrimeNumbers_Test(int start, int ende, int threads, int[] expected)
    {
        PrimeNumberCalculator primeNumberCalculator = new PrimeNumberCalculator(start, ende, threads);
        List<int> obj = primeNumberCalculator.CalculatedPrimeNumbers(new CancellationToken()).Result;
        Assert.That(obj, Is.EquivalentTo(expected));
    }
}
