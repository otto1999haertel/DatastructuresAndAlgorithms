public interface IElement<T>
{
    T Accept(IVisitor<T> visitor);
}