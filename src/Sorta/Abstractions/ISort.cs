namespace Sorta.Abstractions
{
    public interface ISort
    {
        string Algorithm { get; }
        void Sort(ISortContext context);
    }
}
