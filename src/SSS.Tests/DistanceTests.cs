using SSS.Abstraction;
using System;
using System.Text;

namespace SSS.Tests;

[TestClass]
public abstract class StringDistanceTestBase : StringTestBase
{
    protected sealed override void AppendString(StringBuilder builder, string str) => builder.AppendLine($"{str}: {GetStringDistance().Distance(InternalStringTestVariables.REFERENCE, str)}");

    protected abstract IStringDistance GetStringDistance();
}

[TestClass]
public sealed class CosineDistanceTest : StringDistanceTestBase
{
    protected override IStringDistance GetStringDistance() => Cosine.Default;
}

[TestClass]
public sealed class DamerauDistanceTest : StringDistanceTestBase
{
    protected override IStringDistance GetStringDistance() => Damerau.Default;
}

[TestClass]
public sealed class JaccardDistanceTest : StringDistanceTestBase
{
    protected override IStringDistance GetStringDistance() => Jaccard.Default;
}

[TestClass]
public sealed class JaroWinklerDistanceTest : StringDistanceTestBase
{
    protected override IStringDistance GetStringDistance() => JaroWinkler.Default;
}

[TestClass]
public sealed class LevenshteinDistanceTest : StringDistanceTestBase
{
    protected override IStringDistance GetStringDistance() => Levenshtein.Default;
}

[TestClass]
public sealed class LongestCommonSubsequenceDistanceTest : StringDistanceTestBase
{
    protected override IStringDistance GetStringDistance() => LongestCommonSubsequence.Default;
}

[TestClass]
public sealed class MetricLcsDistanceTest : StringDistanceTestBase
{
    protected override IStringDistance GetStringDistance() => MetricLcs.Default;
}

[TestClass]
public sealed class NGramDistanceTest : StringDistanceTestBase
{
    protected override IStringDistance GetStringDistance() => NGram.Default;
}

[TestClass]
public sealed class NormalizedLevenshteinDistanceTest : StringDistanceTestBase
{
    protected override IStringDistance GetStringDistance() => NormalizedLevenshtein.Default;
}

[TestClass]
public sealed class OptimalStringAlignmentDistanceTest : StringDistanceTestBase
{
    protected override IStringDistance GetStringDistance() => OptimalStringAlignment.Default;
}

[TestClass]
public sealed class QGramDistanceTest : StringDistanceTestBase
{
    protected override IStringDistance GetStringDistance() => QGram.Default;
}

[TestClass]
public sealed class RatcliffObershelpDistanceTest : StringDistanceTestBase
{
    protected override IStringDistance GetStringDistance() => RatcliffObershelp.Default;
}

[TestClass]
public sealed class SorensenDiceDistanceTest : StringDistanceTestBase
{
    protected override IStringDistance GetStringDistance() => SorensenDice.Default;
}
