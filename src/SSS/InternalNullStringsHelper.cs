using System;

namespace SSS;

public class InternalNullStringsHelper
{
    public static void ThrowIfArgumentsIsNull(string s1, string s2)
    {
#if !NETCOREAPP
        if(s1 == null) throw new ArgumentNullException(nameof(s1));
        if(s2 == null) throw new ArgumentNullException(nameof(s2));
#else
        ArgumentNullException.ThrowIfNull(s1, nameof(s1));
        ArgumentNullException.ThrowIfNull(s2, nameof(s2));
#endif
    }
}
