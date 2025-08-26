namespace SSS.Abstraction;

/// <summary>
/// Contract for string similarity metrics.
/// </summary>
public interface IStringSimilarity
{
    /// <summary>
    /// Computes the similarity between two strings.
    /// </summary>
    /// <param name="s1">The first string. Must not be <see langword="null"/>.</param>
    /// <param name="s2">The second string. Must not be <see langword="null"/>.</param>
    /// <returns>
    /// A similarity value, typically normalized to the [0, 1] range where 1 means identical.
    /// </returns>
    double Similarity(string s1, string s2);
}
