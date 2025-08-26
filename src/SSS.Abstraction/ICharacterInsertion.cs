namespace SSS.Abstraction;

/// <summary>
/// Defines a strategy for computing the cost of inserting a character.
/// </summary>
public interface ICharacterInsertion
{
    /// <summary>
    /// Returns the insertion cost for the specified character.
    /// </summary>
    /// <param name="c">The character being inserted.</param>
    /// <returns>A non-negative insertion cost.</returns>
    double InsertionCost(char c);
}
