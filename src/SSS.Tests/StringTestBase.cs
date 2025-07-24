using System;
using System.Text;

namespace SSS.Tests;

[TestClass]
public abstract class StringTestBase
{
    [TestMethod]
    public void DoTest()
    {
        StringBuilder builder = new(256);
        foreach(var str in InternalStringTestVariables.s_Strings) AppendString(builder, str);
        Console.WriteLine(builder.ToString());
    }

    protected abstract void AppendString(StringBuilder builder, string str);
}
