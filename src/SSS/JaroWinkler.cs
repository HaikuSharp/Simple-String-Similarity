using SSS.Abstraction;
using System;
using System.Linq;

namespace SSS;

/// <summary>
/// Jaro–Winkler similarity/distance with configurable threshold.
/// </summary>
public class JaroWinkler(double threshold) : IStringSimilarity, IStringDistance
{
    private const double DEFAULT_THRESHOLD = 0.7;
    private const int THREE = 3;
    private const double JW_COEF = 0.1;

    /// <summary>
    /// Initializes a new instance with the default threshold.
    /// </summary>
    public JaroWinkler() : this(DEFAULT_THRESHOLD) { }

    private double Threshold => threshold;

    /// <summary>
    /// Gets a shared default instance.
    /// </summary>
    public static JaroWinkler Default => field ??= new();

    /// <inheritdoc/>
    public double Similarity(string s1, string s2)
    {
        InternalNullStringsHelper.ThrowIfArgumentsIsNull(s1, s2);

        if(s1.SequenceEqual(s2)) return 1f;

        int[] mtp = Matches(s1, s2);
        float m = mtp[0];

        if(m == 0) return 0f;
        
        double j = ((m / s1.Length + m / s2.Length + (m - mtp[1]) / m)) / THREE;

        return j > Threshold ? j + Math.Min(JW_COEF, 1.0 / mtp[THREE]) * mtp[2] * (1 - j) : j;
    }

    /// <inheritdoc/>
    public double Distance(string s1, string s2) => 1.0 - Similarity(s1, s2);

    private static int[] Matches(string s1, string s2)
    {
        string max, min;

        if(s1.Length > s2.Length)
        {
            max = s1;
            min = s2;
        }
        else
        {
            max = s2;
            min = s1;
        }

        int range = Math.Max(max.Length / 2 - 1, 0);


        int[] matchIndexes = [.. Enumerable.Repeat(-1, min.Length)];
        bool[] matchFlags = new bool[max.Length];
        int matches = 0;

        for(int mi = 0; mi < min.Length; mi++)
        {
            var c1 = min[mi];

            for(int xi = Math.Max(mi - range, 0), xn = Math.Min(mi + range + 1, max.Length); xi < xn; xi++)
            {
                if(matchFlags[xi] || !c1.Equals(max[xi])) continue;
                matchIndexes[mi] = xi;
                matchFlags[xi] = true;
                matches++;
                break;
            }
        }

        char[] ms1 = new char[matches];
        char[] ms2 = new char[matches];

        for(int i = 0, si = 0; i < min.Length; i++)
        {
            if(matchIndexes[i] == -1) continue;
            ms1[si] = min[i];
            si++;
        }

        for(int i = 0, si = 0; i < max.Length; i++)
        {
            if(!matchFlags[i]) continue;
            ms2[si] = max[i];
            si++;
        }

        int transpositions = 0;

        for(int mi = 0; mi < ms1.Length; mi++) if(!ms1[mi].Equals(ms2[mi])) transpositions++;

        int prefix = 0;

        for(int mi = 0; mi < min.Length; mi++)
        {
            if(!s1[mi].Equals(s2[mi])) break;
            prefix++;
        }

        return [matches, transpositions / 2, prefix, max.Length];
    }
}
