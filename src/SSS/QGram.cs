using SSS.Abstraction;
using System;
using System.Collections.Generic;

namespace SSS;

public class QGram : ShingleBase, IStringDistance
{
    public QGram(int k) : base(k) { }

    public QGram() { }

    public static QGram Default => field ??= new();

    public double Distance(string s1, string s2)
    {
        InternalNullStringsHelper.ThrowIfArgumentsIsNull(s1, s2);

        if(s1.Equals(s2)) return 0;

        var profile1 = GetProfile(s1);
        var profile2 = GetProfile(s2);

        int maxPossibleDistance = profile1.Count + profile2.Count;
        return maxPossibleDistance == 0 ? 0 : Distance(profile1, profile2) / maxPossibleDistance;
    }

    public static double Distance(IDictionary<string, int> profile1, IDictionary<string, int> profile2)
    {
        var union = new HashSet<string>();
        union.UnionWith(profile1.Keys);
        union.UnionWith(profile2.Keys);

        int agg = 0;
        foreach(var key in union)
        {
            int v1 = 0;
            int v2 = 0;

            if(profile1.TryGetValue(key, out var iv1)) v1 = iv1;
            if(profile2.TryGetValue(key, out var iv2)) v2 = iv2;

            agg += Math.Abs(v1 - v2);
        }

        return agg;
    }
}
