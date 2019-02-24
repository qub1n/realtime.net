using System;
using System.Collections.Generic;
using System.Text;

namespace RealTime
{
    public class RealTimeService
    {
        public int GetValue(string value)
        {
            ReadOnlySpan<char> span = value.AsSpan();
            return span.Substring(';').Length;
        }
    }

    public static class SpanExtension
    {
        public static ReadOnlySpan<char> Substring(this ReadOnlySpan<char> span, char c)
        {
            int index = span.IndexOf(c);
            return span.Slice(index);
        }
    }
}
