using SSS.Abstraction;
using System;

namespace SSS;

/// <summary>
/// N-gram distance using character n-gram comparison.
/// </summary>
public class NGram(int n) : IStringDistance
{
    private const int DEFAULT_N = 2;
    private readonly int m_N = n;

    /// <summary>
    /// Initializes a new instance with the default n (2).
    /// </summary>
    public NGram() : this(DEFAULT_N) { }

    /// <summary>
    /// Gets a shared default instance.
    /// </summary>
    public static NGram Default => field ??= new();

    /// <inheritdoc/>
    public double Distance(string s1, string s2)
    {
        InternalNullStringsHelper.ThrowIfArgumentsIsNull(s2, s2);

        if(s1.Equals(s2)) return 0;

        const char special = '\n';
        int sl = s1.Length;
        int tl = s2.Length;

        if(sl == 0 || tl == 0) return 1;

        int cost = 0;
        int n = m_N;

        if(sl < n || tl < n)
        {
            for(int i1 = 0, ni = Math.Min(sl, tl); i1 < ni; i1++) if(s1[i1] == s2[i1]) cost++;
            return (float)cost / Math.Max(sl, tl);
        }

        char[] sa = new char[sl + n - 1];

        for(int i1 = 0; i1 < sa.Length; i1++) sa[i1] = i1 < n - 1 ? special : s1[i1 - n + 1];

        float[] p = new float[sl + 1];
        float[] d = new float[sl + 1];

        char[] tj = new char[n];

        int i;
        int j;

        for(i = 0; i <= sl; i++) p[i] = i;

        for(j = 1; j <= tl; j++)
        {
            if(j < n)
            {
                for(int ti = 0; ti < n - j; ti++) tj[ti] = special;
                for(int ti = n - j; ti < n; ti++) tj[ti] = s2[ti - (n - j)];
            }
            else tj = s2.Substring(j - n, n).ToCharArray();
            
            d[0] = j;

            for(i = 1; i <= sl; i++)
            {
                cost = 0;
                int tn = n;

                for(int ni = 0; ni < n; ni++)
                {
                    if(sa[i - 1 + ni] != tj[ni]) cost++;
                    else if(sa[i - 1 + ni] == special) tn--;
                }

                float ec = (float)cost / tn;
                d[i] = Math.Min(Math.Min(d[i - 1] + 1, p[i] + 1), p[i - 1] + ec);
            }

            (p, d) = (d, p);
        }

        return p[sl] / Math.Max(tl, sl);
    }
}
