using SSS.Abstraction;
using System.Text;

namespace SSS.Tests;

[TestClass]
public abstract class StringSimilarityTestBase : StringTestBase
{
    protected sealed override void AppendString(StringBuilder builder, string str) => builder.AppendLine($"{str}: {GetStringSimilarity().Similarity(InternalStringTestVariables.REFERENCE, str)}");

    protected abstract IStringSimilarity GetStringSimilarity();
}

[TestClass]
public sealed class CosineSimilarityTest : StringSimilarityTestBase
{
    protected override IStringSimilarity GetStringSimilarity() => Cosine.Default;
}

[TestClass]
public sealed class JaccardSimilarityTest : StringSimilarityTestBase
{
    protected override IStringSimilarity GetStringSimilarity() => Jaccard.Default;
}

[TestClass]
public sealed class JaroWinklerSimilarityTest : StringSimilarityTestBase
{
    protected override IStringSimilarity GetStringSimilarity() => JaroWinkler.Default;
}

[TestClass]
public sealed class NormalizedLevenshteinSimilarityTest : StringSimilarityTestBase
{
    protected override IStringSimilarity GetStringSimilarity() => NormalizedLevenshtein.Default;
}

[TestClass]
public sealed class RatcliffObershelpSimilarityTest : StringSimilarityTestBase
{
    protected override IStringSimilarity GetStringSimilarity() => RatcliffObershelp.Default;
}

[TestClass]
public sealed class SorensenDiceSimilarityTest : StringSimilarityTestBase
{
    protected override IStringSimilarity GetStringSimilarity() => SorensenDice.Default;
}