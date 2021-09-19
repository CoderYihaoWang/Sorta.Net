using Sorta.Abstractions;

namespace Sorta.Algorithms
{
    public class InsertionSort : IAlgorithm
    {
        public string Algorithm => "Insertion Sort";

        public void Sort(ISortContext context)
        {
            for (var i = 1; i < context.Length; i++)
            {
                var j = i - 1;
                context.CreateVariable("current");
                context.Copy(i, "current");
                while (j >= 0 && context.Compare(j, "current") == ComparisonResult.Greater)
                {
                    context.Swap(j + 1, j);
                    j--;
                }
                context.Copy("current", j + 1);
            }
        }
    }
}
