using Sorta.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorta.Algorithms
{
    public class QuickSort : IAlgorithm
    {
        public string Algorithm => "Quick Sort";

        public void Sort(ISortContext context) =>
            QuickSortInner(0, context.Length, context);

        private void QuickSortInner(int from, int to, ISortContext context)
        {
            if (from < to)
            {
                var index = Partition(from, to, context);
                QuickSortInner(from, index, context);
                QuickSortInner(index + 1, to, context);
            }
        }

        private static int Partition(int from, int to, ISortContext context)
        {
            var pivot = from;
            var index = pivot + 1;
            for (int i = index; i < to; i++)
            {
                if (context.Compare(i, pivot) != ComparisonResult.Greater)
                {
                    context.Swap(i, index++);
                }
            }
            context.Swap(pivot, index - 1);
            return index - 1;
        }
    }
}
