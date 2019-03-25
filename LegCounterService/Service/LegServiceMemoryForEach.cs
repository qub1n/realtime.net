using System;
using System.Collections.Generic;

namespace LegCounterService.Service
{
    public class LegServiceMemoryForEach
    {
        public int NumberOfLegs(string animalsCommaSeparated)
        {
            ReadOnlyMemory<char> mAnimalsCommaSeparated = animalsCommaSeparated.AsMemory();

            int legs = 0;            

            var animals = Split(mAnimalsCommaSeparated, ',');
            foreach (var animal in animals)
            {
                legs += GetNumberOfLegs(animal);
            }
            return legs;
        }

        public static IEnumerable<ReadOnlyMemory<char>> Split(ReadOnlyMemory<char> mem, char c)
        {
            while (mem.Length > 0)
            {
                int pos = mem.Span.IndexOf(c);
                if (pos > 0)
                {
                    ReadOnlyMemory<char> animal = mem.Slice(0, pos);
                    yield return animal;
                }
                else
                {
                    yield return mem;
                    break;
                }

                mem = mem.Slice(pos + 1);
            }
        }

        private static int GetNumberOfLegs(ReadOnlyMemory<char> memAnimal)
        {
            var animal = memAnimal.Span;
            switch (animal)
            {
                case var dog when dog.SequenceEqual("dog".AsSpan()):
                    return 4;
                case var cat when cat.SequenceEqual("cat".AsSpan()):
                    return 4;
                case var spider when spider.SequenceEqual("spider".AsSpan()):
                    return 8;
                case var bird when bird.SequenceEqual("bird".AsSpan()):
                    return 2;
            }

            throw new NotSupportedException();//$"Unknown animal {animal.ToString()}"
        }
    }
}
