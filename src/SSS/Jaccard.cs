using SSS.Abstraction;
using System;
using System.Linq;

namespace SSS;

/// <summary>
/// Jaccard similarity/distance over k-shingle sets.
/// </summary>
public class Jaccard : ShingleBase, IStringSimilarity, IStringDistance
{
    /// <summary>
    /// Initializes a new instance with the specified shingle size.
    /// </summary>
    /// <param name="k">Shingle size k (must be positive).</param>
    public Jaccard(int k) : base(k) { }

    /// <summary>
    /// Initializes a new instance with default shingle size.
    /// </summary>
    public Jaccard() { }

    /// <summary>
    /// Gets a shared default instance.
    /// </summary>
    public static Jaccard Default => field ??= new();

    /// <inheritdoc/>
    public double Similarity(string s1, string s2)
    {
        InternalNullStringsHelper.ThrowIfArgumentsIsNull(s1, s2);

        if(s1.Equals(s2)) return 1;

        var profile1 = GetProfile(s1);
        var profile2 = GetProfile(s2);

        var unionCount = profile1.Keys.Concat(profile2.Keys).Distinct().Count();

        int inter = profile1.Keys.Count + profile2.Keys.Count - unionCount;

        return 1.0 * inter / unionCount;
    }

    /// <inheritdoc/>
    public double Distance(string s1, string s2) => 1.0 - Similarity(s1, s2);
}
