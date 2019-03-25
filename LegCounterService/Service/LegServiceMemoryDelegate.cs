using System;

namespace LegCounterService.Service
{
    public class LegServiceMemoryDelegate
    {
        public delegate int LegCounter(ReadOnlyMemory<char> animal);

        readonly LegCounter _legCounter;

        public LegServiceMemoryDelegate()
        {
            _legCounter = GetNumberOfLegs;
        }

        public int NumberOfLegs(string animalsCommaSeparated)
        {
            ReadOnlyMemory<char> mAnimalsCommaSeparated = animalsCommaSeparated.AsMemory();
            return Split(mAnimalsCommaSeparated, ',', _legCounter);
        }

        public static int Split(ReadOnlyMemory<char> mem, char c, LegCounter visitor)
        {
            int legs = 0;

            while (mem.Length > 0)
            {
                int pos = mem.Span.IndexOf(c);
                if (pos > 0)
                {
                    ReadOnlyMemory<char> animal = mem.Slice(0, pos);
                    legs += visitor(animal);
                }
                else
                {
                    legs += visitor(mem);
                    break;
                }

                mem = mem.Slice(pos + 1);
            }
            return legs;
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
