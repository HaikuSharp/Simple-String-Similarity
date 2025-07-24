using SSS.Abstraction;
using System;
using System.Linq;

namespace SSS;

public class NormalizedLevenshtein : IStringSimilarity, IStringDistance
{
    public static NormalizedLevenshtein Default => field ??= new();

    public double Distance(string s1, string s2)
    {
        InternalNullStringsHelper.ThrowIfArgumentsIsNull(s1, s2);

        if(s1.SequenceEqual(s2)) return 0.0;

        int length = Math.Max(s1.Length, s2.Length);

        return length == 0 ? 0.0 : Levenshtein.Default.Distance(s1, s2) / length;
    }

    public double Similarity(string s1, string s2) => 1.0 - Distance(s1, s2);
}
