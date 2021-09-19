using System.Collections.Generic;
using System.Linq;

namespace Sorta.Abstractions
{
    public class SortStats
    {
        public IEnumerable<int> Unsorted { get; set; }
        public IEnumerable<int> Sorted { get; set; }
        public IEnumerable<Operation> Steps { get; set; }
        public int Copies { get; set; }
        public int Variables { get; set; }
        public int Comparisons { get; set; }
        public int Swaps { get; set; }
        public int Length { get => Unsorted.Count(); }
        public bool HasCompleted { get; set; }
    }

    public class Operation
    {
        public OperationType Type { get; set; }
        public int? From { get; set; }
        public int? To { get; set; }
        public string FromVar { get; set; }
        public string ToVar { get; set; }
        public string Name { get; set; }
    }

    public enum OperationType
    {
        Compare,
        Swap,
        Copy,
        Create
    }
}
