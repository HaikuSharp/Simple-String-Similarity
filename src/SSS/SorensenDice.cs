using SSS.Abstraction;
using System.Collections.Generic;

namespace SSS;

/// <summary>
/// Sørensen–Dice similarity/distance over k-shingle sets.
/// </summary>
public class SorensenDice : ShingleBase, IStringSimilarity, IStringDistance
{
    /// <summary>
    /// Initializes a new instance with the specified shingle size.
    /// </summary>
    /// <param name="k">Shingle size k (must be positive).</param>
    public SorensenDice(int k) : base(k) { }

    /// <summary>
    /// Initializes a new instance with default shingle size.
    /// </summary>
    public SorensenDice() { }

    /// <summary>
    /// Gets a shared default instance.
    /// </summary>
    public static SorensenDice Default => field ??= new();

    /// <inheritdoc/>
    public double Similarity(string s1, string s2)
    {
        InternalNullStringsHelper.ThrowIfArgumentsIsNull(s1, s2);

        if (s1.Equals(s2)) return 1;

        var profile1 = GetProfile(s1);
        var profile2 = GetProfile(s2);

        var union = new HashSet<string>();
        union.UnionWith(profile1.Keys);
        union.UnionWith(profile2.Keys);
        
        int inter = 0;

        foreach (var key in union) if (profile1.ContainsKey(key) && profile2.ContainsKey(key)) inter++;

        return 2.0 * inter / (profile1.Count + profile2.Count);
    }

    /// <inheritdoc/>
    public double Distance(string s1, string s2) => 1 - Similarity(s1, s2);
}
