public interface IVisitor<T>
{
    //defines what type of expression the visitor supports
    //Operation, die auf Element ausgeführt wird
    T VisitAdder(Adder adder);
}