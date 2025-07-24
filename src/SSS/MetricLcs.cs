using SSS.Abstraction;
using System;
using System.Linq;

namespace SSS;

public class MetricLcs : IStringDistance
{
    public static MetricLcs Default => field ??= new();

    public double Distance(string s1, string s2)
    {
        InternalNullStringsHelper.ThrowIfArgumentsIsNull(s1, s2);

        if(s1.SequenceEqual(s2)) return 0;

        int length = Math.Max(s1.Length, s2.Length);

        return length is 0 ? 0.0 : 1.0 - (1.0 * LongestCommonSubsequence.Length(s1, s2)) / length;
    }

}
