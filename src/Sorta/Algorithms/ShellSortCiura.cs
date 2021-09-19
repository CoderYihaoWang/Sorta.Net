using Sorta.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorta.Algorithms
{
    public class ShellSortCiura : IAlgorithm
    {
        // Ciura gap sequence
        private readonly int[] _gaps = new int[] { 701, 301, 132, 57, 23, 10, 4, 1 };

        public string Algorithm => "Shell Sort with Ciura Sequence";

        public void Sort(ISortContext context)
        {
            foreach (int gap in _gaps)
            {
                for (int i = gap; i < context.Length; i++)
                {
                    context.CreateVariable("temp");
                    context.Copy(i, "temp");
                    int j = i;
                    for (; j >= gap && context.Compare(j - gap, "temp") == ComparisonResult.Greater; j -= gap)
                    {
                        context.Swap(j, j - gap);
                    }
                    context.Copy("temp", j);
                }
            }
        }
    }
}
