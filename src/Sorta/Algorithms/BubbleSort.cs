using Sorta.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorta.Algorithms
{
    public class BubbleSort : IAlgorithm
    {
        public string Algorithm => "Bubble Sort";

        public void Sort(ISortContext context)
        {
            for (int i = 0; i < context.Length - 1; i++)
            {
                for (int j = 0; j < context.Length - i - 1; j++)
                {
                    if (context.Compare(j, j + 1) == ComparisonResult.Greater)
                    {
                        context.Swap(j, j + 1);
                    }
                }
            }
        }
    }
}
