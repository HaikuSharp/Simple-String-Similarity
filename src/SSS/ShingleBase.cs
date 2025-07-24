using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SSS;

public abstract
#if NETCOREAPP
    partial 
#endif
    class ShingleBase(int k)
{
    private const int DEFAULT_K = 3;
    private static readonly Regex m_SpaceRegix =
#if NETCOREAPP
    GetSpaceRegex();
#else
        new("\\s+", RegexOptions.Compiled);
#endif

    protected ShingleBase() : this(DEFAULT_K) { }

    protected int K { get; } = k > 0 ? k : throw new ArgumentOutOfRangeException(nameof(k), "k should be positive!");

    protected internal Dictionary<string, int> GetProfile(string s)
    {
        int lk = K;
        var shingles = new Dictionary<string, int>();
        var stringWithoutSpaces = m_SpaceRegix.Replace(s, " ");

        for(int i = 0; i < (stringWithoutSpaces.Length - lk + 1); i++)
        {
            var shingle = stringWithoutSpaces.Substring(i, lk);
            shingles[shingle] = shingles.TryGetValue(shingle, out var old) ? old + 1 : 1;
        }

        return shingles;
    }

#if NETCOREAPP
    [GeneratedRegex("\\s+", RegexOptions.Compiled)]
    private static partial Regex GetSpaceRegex();
#endif
}
