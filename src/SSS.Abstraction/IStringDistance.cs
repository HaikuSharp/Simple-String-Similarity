namespace SSS.Abstraction;

/// <summary>
/// Contract for string distance metrics.
/// </summary>
public interface IStringDistance
{
    /// <summary>
    /// Computes the distance between two strings.
    /// </summary>
    /// <param name="s1">The first string. Must not be <see langword="null"/>.</param>
    /// <param name="s2">The second string. Must not be <see langword="null"/>.</param>
    /// <returns>
    /// A distance value. The exact scale depends on the algorithm; many implementations return a normalized value in [0, 1].
    /// </returns>
    double Distance(string s1, string s2);
}
