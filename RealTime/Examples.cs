using System;
using System.Linq;
using System.IO;

namespace RealTime
{
    public static class Examples
    {
        public static void ExampleString()
        {
            string s = "dog,cat,spider";
            string dog = s.Substring(0, 3);
            string cat = s.Substring(4, 3);
            string spider = s.Substring(8, 6);
        }

        public static void ExampleSpan()
        {
            string s = "dog,cat,spider";
            ReadOnlySpan<char> span = s.AsSpan();
            ReadOnlySpan<char> dog = span.Slice(0, 3);
            ReadOnlySpan<char> cat = span.Slice(4, 3);
            ReadOnlySpan<char> spider = span.Slice(8, 6);
        }

        unsafe public static void ExampleMemory()
        {
            string s = "dog,cat,spider";
            ReadOnlyMemory<char> memory = s.AsMemory();
            ReadOnlyMemory<char> dog = memory.Slice(0, 3);
            ReadOnlyMemory<char> cat = memory.Slice(4, 3);
            ReadOnlyMemory<char> spider = memory.Slice(8, 6);
        }

        public static int GetNumberOfLegsInFile(string path)
        {
            string content = File.ReadAllText(path);
            var animals = content.Split(',', StringSplitOptions.RemoveEmptyEntries);
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
