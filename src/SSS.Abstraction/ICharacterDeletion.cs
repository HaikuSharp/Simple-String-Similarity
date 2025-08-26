namespace SSS.Abstraction;

/// <summary>
/// Defines a strategy for computing the cost of deleting a character.
/// </summary>
public interface ICharacterDeletion
{
    /// <summary>
    /// Returns the deletion cost for the specified character.
    /// </summary>
    /// <param name="c">The character being deleted.</param>
    /// <returns>A non-negative deletion cost.</returns>
    double DeletionCost(char c);
}
