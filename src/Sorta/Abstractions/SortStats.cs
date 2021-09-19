using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Sorta.Abstractions
{
    public class SortStats
    {
        public IEnumerable<int> Unsorted { get; set; }
        public IEnumerable<int> Sorted { get; set; }
        public IEnumerable<Operation> Steps { get; set; }
        public int Copies { get; set; }
        public int TemporaryVariables { get; set; }
        public int Comparisons { get; set; }
        public int Swaps { get; set; }
        public int Length { get => Unsorted.Count(); }
        public bool IsSuccessful { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class Operation
    {
        public OperationType Type { get; set; }
        public int? From { get; set; }
        public int? To { get; set; }
        public string FromVar { get; set; }
        public string ToVar { get; set; }
    }

    public enum OperationType
    {
        Compare,
        Swap,
        Copy
    }
}
