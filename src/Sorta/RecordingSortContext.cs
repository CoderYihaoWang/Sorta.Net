using Sorta.Abstractions;
using System.Collections.Generic;
using System.Linq;

namespace Sorta
{
    public interface IRecordingSortContext : ISortContext
    {
        SortStats Results { get; }
    }

    public class RecordingSortContext : IRecordingSortContext
    {
        private readonly int _maxSteps;
        private readonly int[] _input;
        private readonly int[] _data;
        private readonly IList<Operation> _steps;
        private readonly IDictionary<string, int> _vars;
        private int _swaps;
        private int _comparisons;
        private int _copies;
        private int _variables;
        private bool _hasCompleted;

        public int Length => _input.Length;
        public SortStats Results 
        {
            get 
            {
                return new SortStats
                {
                    Unsorted = _input.ToArray(),
                    Sorted = _data.ToArray(),
                    Steps = _steps.ToList(),
                    Comparisons = _comparisons,
                    Swaps = _swaps,
                    Copies = _copies,
                    Variables = _variables,
                    HasCompleted = _hasCompleted
                };
            } 
        }

        public RecordingSortContext(IEnumerable<int> data, int maxSteps)
        {
            _maxSteps = maxSteps;
            _input = data.ToArray();
            _data = data.ToArray();
            _steps = new List<Operation>();
            _vars = new Dictionary<string, int>();
            _hasCompleted = true;
        }

        public void CreateVariable(string name)
        {
            EnsureSteps();

            if (_vars.ContainsKey(name))
            {
                _vars[name] = 0;
            }
            else
            {
                _vars.Add(name, 0);
                _variables++;
            }
            _steps.Add(new Operation
            {
                Type = OperationType.Create,
                Name = name
            });
        }

        #region Copy

        public void Copy(int from, int to)
        {
            EnsureSteps();
            
            _copies++;
            _steps.Add(new Operation
            {
                Type = OperationType.Copy,
                From = from,
                To = to
            });
            _data[to] = _data[from];
        }

        public void Copy(string from, int to)
        {
            EnsureSteps();

            _copies++;
            _steps.Add(new Operation
            {
                Type = OperationType.Copy,
                FromVar = from,
                To = to
            });
            _data[to] = _vars[from];
        }

        public void Copy(int from, string to)
        {
            EnsureSteps();

            _copies++;
            _steps.Add(new Operation
            {
                Type = OperationType.Copy,
                From = from,
                ToVar = to
            });
            _vars[to] = _data[from];
        }

        public void Copy(string from, string to)
        {
            EnsureSteps();

            _copies++;
            _steps.Add(new Operation
            {
                Type = OperationType.Copy,
                FromVar = from,
                ToVar = to
            });
            _vars[to] = _vars[from];
        }

        #endregion Copy

        #region Compare


        public ComparisonResult Compare(int from, int to)
        {
            EnsureSteps();

            _comparisons++;
            _steps.Add(new Operation
            {
                Type = OperationType.Compare,
                From = from,
                To = to
            });
            return (_data[from] - _data[to]) switch
            {
                > 0 => ComparisonResult.Greater,
                < 0 => ComparisonResult.Smaller,
                _ => ComparisonResult.Equal
            };
        }

        public ComparisonResult Compare(string from, int to)
        {
            EnsureSteps();

            _comparisons++;
            _steps.Add(new Operation
            {
                Type = OperationType.Compare,
                FromVar = from,
                To = to
            });
            return (_vars[from] - _data[to]) switch
            {
                > 0 => ComparisonResult.Greater,
                < 0 => ComparisonResult.Smaller,
                _ => ComparisonResult.Equal
            };
        }

        public ComparisonResult Compare(int from, string to)
        {
            EnsureSteps();

            _comparisons++;
            _steps.Add(new Operation
            {
                Type = OperationType.Compare,
                From = from,
                ToVar = to
            });
            return (_data[from] - _vars[to]) switch
            {
                > 0 => ComparisonResult.Greater,
                < 0 => ComparisonResult.Smaller,
                _ => ComparisonResult.Equal
            };
        }

        public ComparisonResult Compare(string from, string to)
        {
            EnsureSteps();

            _comparisons++;
            _steps.Add(new Operation
            {
                Type = OperationType.Compare,
                FromVar = from,
                ToVar = to
            });
            return (_vars[from] - _vars[to]) switch
            {
                > 0 => ComparisonResult.Greater,
                < 0 => ComparisonResult.Smaller,
                _ => ComparisonResult.Equal
            };
        }

        #endregion Compare

        #region Swap

        public void Swap(int from, int to)
        {
            EnsureSteps();

            _swaps++;
            _copies += 2;

            _steps.Add(new Operation
            {
                Type = OperationType.Swap,
                From = from,
                To = to
            });

            (_data[from], _data[to]) = (_data[to], _data[from]);
        }

        public void Swap(string from, int to)
        {
            EnsureSteps();

            _swaps++;
            _copies += 2;

            _steps.Add(new Operation
            {
                Type = OperationType.Swap,
                FromVar = from,
                To = to
            });

            (_vars[from], _data[to]) = (_data[to], _vars[from]);
        }

        public void Swap(int from, string to)
        {
            EnsureSteps();

            _swaps++;
            _copies += 2;

            _steps.Add(new Operation
            {
                Type = OperationType.Swap,
                From = from,
                ToVar = to
            });

            (_data[from], _vars[to]) = (_vars[to], _data[from]);
        }

        public void Swap(string from, string to)
        {
            EnsureSteps();

            _swaps++;
            _copies += 2;

            _steps.Add(new Operation
            {
                Type = OperationType.Swap,
                FromVar = from,
                ToVar = to
            });

            (_vars[from], _vars[to]) = (_vars[to], _vars[from]);
        }

        #endregion Swap

        private void EnsureSteps()
        {
            if (_steps.Count >= _maxSteps)
            {
                _hasCompleted = false;
                throw new MaxStepsReachedException(Results);
            }
        }
    }
}
