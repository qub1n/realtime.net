using System;

namespace RealTime
{
    public class RealTimeService
    {
        public delegate int LegCounter(ReadOnlySpan<char> animal);

        LegCounter _legCounter;

        public RealTimeService()
        {
            _legCounter = GetNumberOfLegs;
        }

        public int NumberOfLegs(string animalsCommaSeparated)
        {
            return Split(animalsCommaSeparated.AsSpan(), ',', _legCounter);
        }

        private static int GetNumberOfLegs(ReadOnlySpan<char> animal)
        {
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

            //if (animal.SequenceEqual("dog".AsSpan()))
            //    return 4;
            //if (animal.SequenceEqual("cat".AsSpan()))
            //    return 4;
            //if (animal.SequenceEqual("spider".AsSpan()))
            //    return 8;
            //if (animal.SequenceEqual("bird".AsSpan()))
            //    return 2;

            throw new NotSupportedException();//$"Uknown animal {animal.ToString()}"
        }



        public static int Split(ReadOnlySpan<char> span, char c, LegCounter visitor)
        {
            int legs = 0;

            while (span.Length > 0)
            {
                int pos = span.IndexOf(c);
                if (pos > 0)
                {
                    ReadOnlySpan<char> animal = span.Slice(0, pos);
                    legs += visitor(animal);
                }
                else
                {
                    legs += visitor(span);
                    break;
                }

                span = span.Slice(pos + 1);
            }
            return legs;
        }
    }
}
