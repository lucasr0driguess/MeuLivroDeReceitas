using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace WebApi.Tests.InlineData;

internal class CultureInlineData : IEnumerable<object[]>
{
    {
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] {"en-US"};
        yield return new object[]{"pt-BR"};
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

}
