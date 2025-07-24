using SSS.Abstraction;
using System;
using System.Collections.Generic;

namespace SSS;

public class RatcliffObershelp : IStringSimilarity, IStringDistance
{
    public static RatcliffObershelp Default => field ??= new();

    public double Similarity(string s1, string s2)
    {
        InternalNullStringsHelper.ThrowIfArgumentsIsNull(s1, s2);

        if (s1.Equals(s2)) return 1.0d;

        var matches = GetMatchList(s1, s2);
        int sumOfMatches = 0;

        foreach (var match in matches)
            sumOfMatches += match.Length;

        return 2.0d * sumOfMatches / (s1.Length + s2.Length);
    }

    public double Distance(string s1, string s2) => 1.0d - Similarity(s1, s2);
    

    private static List<string> GetMatchList(string s1, string s2)
    {
        var list = new List<string>();
        var match = FrontMaxMatch(s1, s2);

        if (match.Length > 0)
        {
            var frontSource = s1.Substring(0, s1.IndexOf(match, StringComparison.Ordinal));
            var frontTarget = s2.Substring(0, s2.IndexOf(match, StringComparison.Ordinal));
            var frontQueue = GetMatchList(frontSource, frontTarget);

            var endSource = s1.Substring(s1.IndexOf(match, StringComparison.Ordinal) + match.Length);
            var endTarget = s2.Substring(s2.IndexOf(match, StringComparison.Ordinal) + match.Length);
            var endQueue = GetMatchList(endSource, endTarget);

            list.Add(match.ToString());
            list.AddRange(frontQueue);
            list.AddRange(endQueue);
        }

        return list;
    }

    private static string FrontMaxMatch(string s1, string s2)
    {
        int longest = 0;
        var longestSubstring = string.Empty;

        for (int i = 0; i < s1.Length; ++i) for (int j = i + 1; j <= s1.Length; ++j)
        {
            var substring = s1.Substring(i, j - i);
            if (s2.IndexOf(substring, StringComparison.Ordinal) is not -1 && substring.Length > longest)
            {
                longest = substring.Length;
                longestSubstring = substring;
            }
        }

        return longestSubstring;
    }
}