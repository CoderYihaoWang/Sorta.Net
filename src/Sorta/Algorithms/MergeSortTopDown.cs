using Sorta.Abstractions;

namespace Sorta.Algorithms
{
    public class MergeSortTopDown : IAlgorithm
    {
        public string Algorithm => "Merge Sort Top Down";

        public void Sort(ISortContext context)
        {
            MergeSortInner(0, context.Length, context);
        }

        private void MergeSortInner(int from, int to, ISortContext context)
        {
            if (to - from < 2)
            {
                return;
            }

            var middle = from + (to - from) / 2;
            MergeSortInner(from, middle, context);
            MergeSortInner(middle, to, context);
            Merge(from, middle, middle, to, context);
        }

        private static void Merge(int leftFrom, int leftTo, int rightFrom, int rightTo, ISortContext context)
        {
            int count = 0;
            int from = leftFrom;
            while (leftFrom < leftTo && rightFrom < rightTo)
            {
                context.CreateVariable($"temp{count}");
                if (context.Compare(leftFrom, rightFrom) == ComparisonResult.Smaller)
                {
                    context.Copy(leftFrom++, $"temp{count++}");
                }
                else
                {
                    context.Copy(rightFrom++, $"temp{count++}");
                }
            }

            while (leftFrom < leftTo)
            {
                context.CreateVariable($"temp{count}");
                context.Copy(leftFrom++, $"temp{count++}");
            }

            while (rightFrom < rightTo)
            {
                context.CreateVariable($"temp{count}");
                context.Copy(rightFrom++, $"temp{count++}");
            }

            for (int i = rightTo - 1; i >= from; i--)
            {
                context.Copy($"temp{--count}", i);
            }
        }
    }
}
