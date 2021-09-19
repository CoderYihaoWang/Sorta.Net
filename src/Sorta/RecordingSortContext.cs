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
        private readonly int[] _input;
        private readonly int[] _data;
        private readonly IList<Operation> _steps;
        private int _swaps;
        private int _comparisons;
        private int _copies;
        private int _variables;
        private string _errorMessage;

        private readonly IDictionary<string, int> _tempVars;

        private bool _hasTerminated;
        private bool _hasSwapped;

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
                    TemporaryVariables = _variables,
                    ErrorMessage = _errorMessage,
                    IsSuccessful = string.IsNullOrEmpty(_errorMessage)
                };
            } 
        }

        public int Length => _input.Length;

        public RecordingSortContext(IEnumerable<int> data)
        {
            _input = data.ToArray();
            _data = data.ToArray();
            _steps = new List<Operation>();
            _tempVars = new Dictionary<string, int>();
        }

        public void CreateVariable(string name)
        {
            if (_tempVars.ContainsKey(name))
            {
                _tempVars[name] = 0;
            }
            else
            {
                _tempVars.Add(name, 0);
                _variables++;
            }
        }

        public void Copy(int from, int to)
        {
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
            _copies++;
            _steps.Add(new Operation
            {
                Type = OperationType.Copy,
                FromVar = from,
                To = to
            });
            _data[to] = _tempVars[from];
        }

        public void Copy(int from, string to)
        {
            _copies++;
            _steps.Add(new Operation
            {
                Type = OperationType.Copy,
                From = from,
                ToVar = to
            });
            _tempVars[to] = _data[from];
        }

        public void Copy(string from, string to)
        {
            _copies++;
            _steps.Add(new Operation
            {
                Type = OperationType.Copy,
                FromVar = from,
                ToVar = to
            });
            _tempVars[to] = _tempVars[from];
        }
        
        public ComparisonResult Compare(int from, int to)
        {
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
            _comparisons++;
            _steps.Add(new Operation
            {
                Type = OperationType.Compare,
                FromVar = from,
                To = to
            });
            return (_tempVars[from] - _data[to]) switch
            {
                > 0 => ComparisonResult.Greater,
                < 0 => ComparisonResult.Smaller,
                _ => ComparisonResult.Equal
            };
        }

        public ComparisonResult Compare(int from, string to)
        {
            _comparisons++;
            _steps.Add(new Operation
            {
                Type = OperationType.Compare,
                From = from,
                ToVar = to
            });
            return (_data[from] - _tempVars[to]) switch
            {
                > 0 => ComparisonResult.Greater,
                < 0 => ComparisonResult.Smaller,
                _ => ComparisonResult.Equal
            };
        }

        public ComparisonResult Compare(string from, string to)
        {
            _comparisons++;
            _steps.Add(new Operation
            {
                Type = OperationType.Compare,
                FromVar = from,
                ToVar = to
            });
            return (_tempVars[from] - _tempVars[to]) switch
            {
                > 0 => ComparisonResult.Greater,
                < 0 => ComparisonResult.Smaller,
                _ => ComparisonResult.Equal
            };
        }

        public void Swap(int from, int to)
        {
            _swaps++;
            _copies += 2;

            if (!_hasSwapped)
            {
                _variables++;
            }

            _hasSwapped = true;

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
            _swaps++;
            _copies += 2;

            if (!_hasSwapped)
            {
                _variables++;
            }

            _hasSwapped = true;

            _steps.Add(new Operation
            {
                Type = OperationType.Swap,
                FromVar = from,
                To = to
            });
            (_tempVars[from], _data[to]) = (_data[to], _tempVars[from]);
        }

        public void Swap(int from, string to)
        {
            _swaps++;
            _copies += 2;

            if (!_hasSwapped)
            {
                _variables++;
            }

            _hasSwapped = true;

            _steps.Add(new Operation
            {
                Type = OperationType.Swap,
                From = from,
                ToVar = to
            });
            (_data[from], _tempVars[to]) = (_tempVars[to], _data[from]);
        }

        public void Swap(string from, string to)
        {
            _swaps++;
            _copies += 2;

            if (!_hasSwapped)
            {
                _variables++;
            }

            _hasSwapped = true;

            _steps.Add(new Operation
            {
                Type = OperationType.Swap,
                FromVar = from,
                ToVar = to
            });
            (_tempVars[from], _tempVars[to]) = (_tempVars[to], _tempVars[from]);
        }
    }
}
