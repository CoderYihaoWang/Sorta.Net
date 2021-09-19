using Sorta.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorta.Algorithms
{
    public class SelectionSort : IAlgorithm
    {
        public string Algorithm => "Selection Sort";

        public void Sort(ISortContext context)
        {
            for (int i = 0; i < context.Length - 1; i++)
            {
                var minIndex = i;
                for (int j = i + 1; j < context.Length; j++)
                {
                    if (context.Compare(j, minIndex) == ComparisonResult.Smaller)
                    {
                        minIndex = j;
                    }
                }
                context.Swap(i, minIndex);
            }
        }
    }
}
