using Sorta.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorta.Algorithms
{
    public class ShellSort : IAlgorithm
    {     
        public string Algorithm => "Shell Sort";

        public void Sort(ISortContext context)
        {
            int gap = 1;
            while (gap < context.Length / 3)
            {
                gap = gap * 3 + 1;
            }

            for (; gap > 0; gap /= 3)
            {
                for (int i = gap; i < context.Length; i++)
                {
                    context.CreateVariable("temp");
                    context.Copy(i, "temp");
                    var j = i - gap;
                    for (; j >= 0 && context.Compare(j, "temp") == ComparisonResult.Greater; j -= gap)
                    {
                        context.Swap(j + gap, j);
                    }
                    context.Copy("temp", j + gap);
                }
            }
        }
    }
}
