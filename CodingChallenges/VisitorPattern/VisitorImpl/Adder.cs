public class Adder : IElement<double>
{
    //Defines what Data the visitor will use for the operation
    public List<double> _numbersToAdd{ get; private set; }
    public Adder (List<double> NumbersToAdd)
    {
        _numbersToAdd = NumbersToAdd?.ToList() ?? new List<double>();
    }
    public double Accept(IVisitor<double> visitor)
    {
        return visitor.VisitAdder(this);
    }
}