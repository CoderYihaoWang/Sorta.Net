using Sorta.Abstractions;
using System;

namespace Sorta
{
    public class MaxStepsReachedException : Exception
    {
        private readonly SortStats _results;
        
        public MaxStepsReachedException(SortStats results)
        {
            _results = results;
        }

        public SortStats Results { get => _results; }
    }
}
