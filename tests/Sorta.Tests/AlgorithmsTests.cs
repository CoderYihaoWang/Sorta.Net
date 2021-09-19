using Sorta.Abstractions;
using Sorta.Algorithms;
using System.Collections.Generic;
using Xunit;

namespace Sorta.Tests.AlgorithmsTests
{
    public class AlgorithmsTests
    {
        public static IEnumerable<object[]> Data => new List<object[]>
        {
            new object[] { new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 } },
            new object[] { new int[] { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1} },
            new object[] { new int[] { 0, 6, 4, 3, 8, 9, 1, 2, 3, 5} },
            new object[] { new int[] { 1, 1, 1, 3, 3, 3, 6, 6, 0, 0, 0, 0} },
        };

        [Theory]
        [MemberData(nameof(Data))]
        public void BubbleSortTests(int[] data) => TestAlgorithm(new BubbleSort(), data);

        [Theory]
        [MemberData(nameof(Data))]
        public void InsertionSortTests(int[] data) => TestAlgorithm(new InsertionSort(), data);

        [Theory]
        [MemberData(nameof(Data))]
        public void SelectionSortTests(int[] data) => TestAlgorithm(new SelectionSort(), data);

        private void TestAlgorithm(IAlgorithm sort, int[] data)
        {
            var context = new TestSortContext(data);
            sort.Sort(context);
            context.AssertSorted();
        }
    }
}
