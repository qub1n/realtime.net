using System;
using System.Collections.Generic;
using System.Text;

namespace RealTime
{
    class NonRealTimeService
    {
        public int GetValue(string value)
        {
            var index = value.IndexOf(';');
            string substr = value.Substring(index);
            return substr.Length;
        }
    }
}
