public class ConcreteVisitor : IVisitor<double>
{
    public double VisitAdder(Adder adder)
    {
        return adder._numbersToAdd.Sum();
    }
}