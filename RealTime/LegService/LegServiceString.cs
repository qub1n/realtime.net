using System;
using System.Linq;

namespace RealTime
{
    class LegServiceString
    {
        public int NumberOfLegs(string animalsCommaSeparated)
        {
            var animals = animalsCommaSeparated.Split(',', StringSplitOptions.RemoveEmptyEntries);
            return animals.Sum(animal => GetNumberOfLegs(animal));
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
