using System;

namespace Sorta.Abstractions
{
    public interface ISortContext
    {
        int Length { get; }
        void CreateVariable(string name);
        void Copy(int from, int to);
        void Copy(string from, int to);
        void Copy(int from, string to);
        void Copy(string from, string to);
        ComparisonResult Compare(int from, int to);
        ComparisonResult Compare(string from, int to);
        ComparisonResult Compare(int from, string to);
        ComparisonResult Compare(string from, string to);
        void Swap(int from, int to);
        void Swap(string from, int to);
        void Swap(int from, string to);
        void Swap(string from, string to);
    }
}
