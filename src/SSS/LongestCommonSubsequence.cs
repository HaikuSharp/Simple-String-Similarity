using SSS.Abstraction;
using System;
using System.Linq;

namespace SSS;

public class LongestCommonSubsequence : IStringDistance
{
    public static LongestCommonSubsequence Default => field ??= new();

    public double Distance(string s1, string s2)
    {
        InternalNullStringsHelper.ThrowIfArgumentsIsNull(s1, s2);
        if(s1.SequenceEqual(s2)) return 0;
        int maxLen = Math.Max(s1.Length, s2.Length);
        return (s1.Length + s2.Length - 2 * InternalLength(s1, s2)) / (double)maxLen;
    }

    public static int Length(string s1, string s2)
    {
        InternalNullStringsHelper.ThrowIfArgumentsIsNull(s1, s2);
        return InternalLength(s1, s2);
    }

    private static int InternalLength(string s1, string s2)
    {
        int s1_length = s1.Length;
        int s2_length = s2.Length;

        int[,] c = new int[s1_length + 1, s2_length + 1];

        for(int i = 0; i <= s1_length; i++) c[i, 0] = 0;
        for(int j = 0; j <= s2_length; j++) c[0, j] = 0;
        for(int i = 1; i <= s1_length; i++)for(int j = 1; j <= s2_length; j++) c[i, j] = s1[i - 1].Equals(s2[j - 1]) ? c[i - 1, j - 1] + 1 : Math.Max(c[i, j - 1], c[i - 1, j]);

        return c[s1_length, s2_length];
    }
}