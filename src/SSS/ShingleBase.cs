using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SSS;

/// <summary>
/// Base class for shingle-based string metrics.
/// </summary>
public abstract
#if NETCOREAPP
    partial 
#endif
    class ShingleBase(int k)
{
    private const int DEFAULT_K = 3;
    private static readonly Regex s_SpaceRegix =
#if NETCOREAPP
    GetSpaceRegex();
#else
        new("\\s+", RegexOptions.Compiled);
#endif

    /// <summary>
    /// Initializes a new instance with default shingle size (k = 3).
    /// </summary>
    protected ShingleBase() : this(DEFAULT_K) { }

    /// <summary>
    /// Gets the shingle size k. Must be positive.
    /// </summary>
    protected int K { get; } = k > 0 ? k : throw new ArgumentOutOfRangeException(nameof(k), "k should be positive!");

    /// <summary>
    /// Builds a shingle frequency profile for the specified string using size <see cref="K"/>.
    /// Multiple whitespace characters are normalized to a single space before shingling.
    /// </summary>
    /// <param name="s">The input string.</param>
    /// <returns>Dictionary mapping shingle to its frequency.</returns>
    protected internal Dictionary<string, int> GetProfile(string s)
    {
        int lk = K;
        var shingles = new Dictionary<string, int>();
        var stringWithoutSpaces = s_SpaceRegix.Replace(s, " ");

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
