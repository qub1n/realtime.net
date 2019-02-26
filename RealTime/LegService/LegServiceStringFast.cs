using System;
using System.Collections.Generic;
using System.Text;

namespace RealTime
{
    class LegServiceStringFast
    {
        public delegate int LegCounter(string animal);

        public int NumberOfLegs(string animalsCommaSeparated)
        {
            return Split(animalsCommaSeparated, ',', GetNumberOfLegs);
        }

        public static int Split(string s, char c, LegCounter visitor)
        {
            int legs = 0;
            int start = 0;

            while (s.Length > 0)
            {
                int pos = s.IndexOf(c, start);
                if (pos > 0)
                {
                    string animal = s.Substring(start, pos - start);
                    legs += visitor(animal);
                }
                else
                {                    
                    break;
                }

                start = pos + 1;
            }
            return legs;
        }

        private static int GetNumberOfLegs(string animal)
        {
            switch (animal)
            {
                case "dog":
                case "cat":
                    return 4;
                case "spider":
                    return 8;
                case "bird":
                    return 2;
                default:
                    throw new NotSupportedException($"Uknown animal {animal}");
            }
        }
    }
}
