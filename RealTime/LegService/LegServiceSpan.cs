﻿using System;

namespace RealTime
{ 
    public class LegServiceSpan
    {        
        public delegate int LegCounter(ReadOnlySpan<char> animal);

        LegCounter _legCounter;

        public LegServiceSpan()
        {
            _legCounter = GetNumberOfLegs;
        }

        public int NumberOfLegs(string animalsCommaSeparated)
        {
            return Split(animalsCommaSeparated.AsSpan(), ',', _legCounter);
        }

        public int NumberOfLegs(ReadOnlySpan<char> animalsCommaSeparated)
        {
            return Split(animalsCommaSeparated, ',', _legCounter);
        }

        public static int Split(ReadOnlySpan<char> span, char c, LegCounter legCounter)
        {
            int legs = 0;

            while (span.Length > 0)
            {
                int pos = span.IndexOf(c);
                if (pos > 0)
                {
                    ReadOnlySpan<char> animal = span.Slice(0, pos);
                    legs += legCounter(animal);
                }
                else
                {
                    legs += legCounter(span);
                    break;
                }
                span = span.Slice(pos + 1);
            }
            return legs;
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
                default:
                    throw new NotSupportedException($"Uknown animal {animal.ToString()}");
            }            
        }
    }
}
