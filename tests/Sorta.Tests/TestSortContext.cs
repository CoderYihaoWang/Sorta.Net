using Sorta.Abstractions;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Sorta.Tests.AlgorithmsTests
{
    public class TestSortContext : ISortContext
    {
        private readonly int[] _data;
        private readonly IDictionary<string, int> _vars;

        public TestSortContext(IEnumerable<int> data)
        {
            _data = data.ToArray();
            _vars = new Dictionary<string, int>();
        }

        public int Length => _data.Length;

        public ComparisonResult Compare(int from, int to) =>
            (_data[from] - _data[to]) switch
            {
                > 0 => ComparisonResult.Greater,
                < 0 => ComparisonResult.Smaller,
                _ => ComparisonResult.Equal
            };

        public ComparisonResult Compare(string from, int to) =>
            (_vars[from] - _data[to]) switch
            {
                > 0 => ComparisonResult.Greater,
                < 0 => ComparisonResult.Smaller,
                _ => ComparisonResult.Equal
            };

        public ComparisonResult Compare(int from, string to) =>
            (_data[from] - _vars[to]) switch
            {
                > 0 => ComparisonResult.Greater,
                < 0 => ComparisonResult.Smaller,
                _ => ComparisonResult.Equal
            };

        public ComparisonResult Compare(string from, string to) =>
            (_vars[from] - _vars[to]) switch
            {
                > 0 => ComparisonResult.Greater,
                < 0 => ComparisonResult.Smaller,
                _ => ComparisonResult.Equal
            };

        public void Copy(int from, int to) => _data[to] = _data[from];

        public void Copy(string from, int to) => _data[to] = _vars[from];

        public void Copy(int from, string to) => _vars[to] = _data[from];

        public void Copy(string from, string to) => _vars[to] = _vars[from];

        public void CreateVariable(string name)
        {
            if (_vars.ContainsKey(name))
            {
                _vars[name] = 0;
            }
            else
            {
                _vars.Add(name, 0);
            }
        }

        public void Swap(int from, int to) => (_data[from], _data[to]) = (_data[to], _data[from]);

        public void Swap(string from, int to) => (_vars[from], _data[to]) = (_data[to], _vars[from]);

        public void Swap(int from, string to) => (_data[from], _vars[to]) = (_vars[to], _data[from]);

        public void Swap(string from, string to) => (_vars[from], _vars[to]) = (_vars[to], _vars[from]);

        public void AssertSorted()
        {
            for (int i = 0, j = 1; j < _data.Length; i++, j++)
            {
                Assert.True(_data[i] <= _data[j]);
            }
        }
    }
}
