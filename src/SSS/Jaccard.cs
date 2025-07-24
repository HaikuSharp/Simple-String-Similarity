using SSS.Abstraction;
using System;
using System.Linq;

namespace SSS;

public class Jaccard : ShingleBase, IStringSimilarity, IStringDistance
{
    public Jaccard(int k) : base(k) { }

    public Jaccard() { }

    public static Jaccard Default => field ??= new();

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

    public double Distance(string s1, string s2) => 1.0 - Similarity(s1, s2);
}
