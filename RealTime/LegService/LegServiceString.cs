using System;
using System.Collections.Generic;
using System.Text;

namespace RealTime
{
    class LegServiceString
    {
        public int NumberOfLegs(string animalsCommaSeparated)
        {
            int legs = 0;
            var animals = animalsCommaSeparated.Split(',', StringSplitOptions.RemoveEmptyEntries);
            foreach (var animal in animals)
            {
                legs += GetNumberOfLegs(animal);
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
