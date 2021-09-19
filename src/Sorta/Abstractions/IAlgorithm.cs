namespace Sorta.Abstractions
{
    public interface IAlgorithm
    {
        string Algorithm { get; }
        void Sort(ISortContext context);
    }
}
