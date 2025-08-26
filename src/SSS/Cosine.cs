using SSS.Abstraction;
using System;
using System.Collections.Generic;

namespace SSS;

/// <summary>
/// Cosine similarity/distance over k-shingle profiles.
/// </summary>
public class Cosine : ShingleBase, IStringSimilarity, IStringDistance
{
    /// <summary>
    /// Initializes a new instance with the specified shingle size.
    /// </summary>
    /// <param name="k">Shingle size k (must be positive).</param>
    public Cosine(int k) : base(k) { }

    /// <summary>
    /// Initializes a new instance with default shingle size.
    /// </summary>
    public Cosine() : base() { }

    /// <summary>
    /// Gets a shared default instance.
    /// </summary>
    public static Cosine Default => field ??= new();

    /// <inheritdoc/>
    public double Similarity(string s1, string s2)
    {
        InternalNullStringsHelper.ThrowIfArgumentsIsNull(s1, s2);

        if(s1.Equals(s2)) return 1;

        int k = K;

        if(s1.Length < k || s2.Length < k) return 0;

        var profile1 = GetProfile(s1);
        var profile2 = GetProfile(s2);

        return DotProduct(profile1, profile2) / (Normalize(profile1) * Normalize(profile2));
    }

    /// <inheritdoc/>
    public double Distance(string s1, string s2) => 1.0 - Similarity(s1, s2);

    private static double Normalize(IDictionary<string, int> profile)
    {
        double agg = 0;
        foreach(var entry in profile) agg += 1.0 * entry.Value * entry.Value;
        return Math.Sqrt(agg);
    }

    private static double DotProduct(IDictionary<string, int> profile1, IDictionary<string, int> profile2)
    {
        var smallProfile = profile2;
        var largeProfile = profile1;

        if(profile1.Count < profile2.Count)
        {
            smallProfile = profile1;
            largeProfile = profile2;
        }

        double agg = 0;

        foreach(var entry in smallProfile)
        {
            if(!largeProfile.TryGetValue(entry.Key, out var i)) continue;
            agg += 1.0 * entry.Value * i;
        }

        return agg;
    }
}
