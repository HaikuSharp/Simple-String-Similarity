using SSS.Abstraction;
using System;

namespace SSS;

/// <summary>
/// Weighted Levenshtein distance where substitution, insertion, and deletion have configurable costs.
/// </summary>
public class WeightedLevenshtein(ICharacterSubstitution characterSubstitution, ICharacterInsertion characterInsertion, ICharacterDeletion characterDeletion) : IStringDistance
{
    /// <inheritdoc/>
    public double Distance(string s1, string s2) => Distance(s1, s2, double.MaxValue);

    /// <summary>
    /// Computes the weighted Levenshtein distance with an early-exit limit.
    /// </summary>
    /// <param name="s1">First string.</param>
    /// <param name="s2">Second string.</param>
    /// <param name="limit">Early-exit threshold; if the minimal possible cost reaches this value, it is returned.</param>
    /// <returns>The weighted edit distance.</returns>
    public double Distance(string s1, string s2, double limit)
    {
        InternalNullStringsHelper.ThrowIfArgumentsIsNull(s1, s2);

        if (s1.Equals(s2)) return 0;

        if (s1.Length == 0) return s2.Length;
        if (s2.Length == 0) return s1.Length;

        double[] v0 = new double[s2.Length + 1];
        double[] v1 = new double[s2.Length + 1];

        v0[0] = 0;
        for (int i = 1; i < v0.Length; i++) v0[i] = v0[i - 1] + InsertionCost(s2[i - 1]);

        for (int i = 0; i < s1.Length; i++)
        {
            char s1i = s1[i];
            double deletionCost = DeletionCost(s1i);

            v1[0] = v0[0] + deletionCost;

            double minv1 = v1[0];
            for (int j = 0; j < s2.Length; j++)
            {
                char s2j = s2[j];
                double cost = 0;
                
                if (s1i != s2j) cost = SubstitutionCost(s1i, s2j);

                double insertionCost = InsertionCost(s2j);

                v1[j + 1] = Math.Min(v1[j] + insertionCost, Math.Min(v0[j + 1] + deletionCost, v0[j] + cost)); 
                minv1 = Math.Min(minv1, v1[j + 1]);
            }

            if (minv1 >= limit) return limit;

            (v0, v1) = (v1, v0);
        }

        return v0[s2.Length];
    }

    private double SubstitutionCost(char c0, char c1) => characterSubstitution.SubstitutionCost(c0, c1);

    private double InsertionCost(char c) => characterInsertion?.InsertionCost(c) ?? 1.0;

    private double DeletionCost(char c) => characterDeletion?.DeletionCost(c) ?? 1.0;
}
