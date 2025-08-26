namespace SSS.Abstraction;

/// <summary>
/// Defines a strategy for computing the cost of substituting one character for another.
/// </summary>
public interface ICharacterSubstitution
{
    /// <summary>
    /// Returns the substitution cost for replacing <paramref name="c1"/> with <paramref name="c2"/>.
    /// </summary>
    /// <param name="c1">The source character.</param>
    /// <param name="c2">The target character.</param>
    /// <returns>A non-negative substitution cost.</returns>
    double SubstitutionCost(char c1, char c2);
}
