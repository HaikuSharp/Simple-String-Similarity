using SSS.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SSS;

public class Damerau : IStringDistance
{
    public static Damerau Default => field ??= new();

    public double Distance(string s1, string s2)
    {
        InternalNullStringsHelper.ThrowIfArgumentsIsNull(s1, s2);

        if(s1.SequenceEqual(s2)) return 0;

        int inf = s1.Length + s2.Length;
        int maxLen = Math.Max(s1.Length, s2.Length);
        var da = new Dictionary<char, int>();

        foreach(char c in s1) da[c] = 0;
        foreach(char c in s2) da[c] = 0;

        int[,] h = new int[s1.Length + 2, s2.Length + 2];

        for(int i = 0; i <= s1.Length; i++)
        {
            h[i + 1, 0] = inf;
            h[i + 1, 1] = i;
        }

        for(int j = 0; j <= s2.Length; j++)
        {
            h[0, j + 1] = inf;
            h[1, j + 1] = j;
        }

        for(int i = 1; i <= s1.Length; i++)
        {
            int db = 0;

            for(int j = 1; j <= s2.Length; j++)
            {
                int i1 = da[s2[j - 1]];
                int j1 = db;

                int cost = 1;

                if(s1[i - 1].Equals(s2[j - 1]))
                {
                    cost = 0;
                    db = j;
                }

                h[i + 1, j + 1] = Min(h[i, j] + cost, h[i + 1, j] + 1, h[i, j + 1] + 1, h[i1, j1] + (i - i1 - 1) + 1 + (j - j1 - 1));
            }

            da[s1[i - 1]] = i;
        }

        return (double)h[s1.Length + 1, s2.Length + 1] / maxLen;
    }

    private static int Min(int a, int b, int c, int d) => Math.Min(a, Math.Min(b, Math.Min(c, d)));
}
