namespace VisitorTest;

public class Tests
{
    private Adder _emptyAdder;
    private Adder _simpleAdder;
    private Adder _decimalAdder;

    private IVisitor<double> _sumVisitor;
    [SetUp]
    public void Setup()
    {
        _emptyAdder = new Adder(new List<double>());
        _simpleAdder = new Adder(new List<double> { 1, 2, 3 });
        _decimalAdder = new Adder(new List<double> { 1.5, 2.7, 3.3 });
        _sumVisitor = new ConcreteVisitor();
    }

    [Test]
    public void Sum_Of_Empty_List_Returns_Zero()
    {
        double result = _emptyAdder.Accept(_sumVisitor);
        Assert.That(result, Is.EqualTo(0.0));
    }

    [Test]
    public void Sum_Of_1_2_3_Returns_6()
    {
        double result = _simpleAdder.Accept(_sumVisitor);
        Assert.That(result, Is.EqualTo(6.0));
    }

    [Test]
    public void Sum_Of_Decimals_Is_Accurate()
    {
        double result = _decimalAdder.Accept(_sumVisitor);
        Assert.That(result, Is.EqualTo(7.5).Within(0.0001));
    }

    [Test]
    public void Adder_Numbers_Are_Immutable_After_Construction()
    {
        var input = new List<double> { 10, 20 };
        var adder = new Adder(input);

        input.Add(30); // Versuche, die Original-Liste zu ändern

        double result = adder.Accept(_sumVisitor);
        Assert.That(result, Is.EqualTo(30.0)); // Sollte 10+20 = 30 sein, nicht 60!
    }

    [Test]
    public void Null_Input_To_Adder_Creates_Empty_List()
    {
        var adder = new Adder(null);
        double result = adder.Accept(_sumVisitor);
        Assert.That(result, Is.EqualTo(0.0));
        Assert.That(adder._numbersToAdd, Is.Not.Null);
    }
}
