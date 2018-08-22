using System;

namespace FM.Engine.Randomise
{

    public class SystemRandom
        : IRandomise
    {

        private readonly Random _random;

        public SystemRandom()
        {
            _random = new Random((int) DateTime.Now.Ticks);
        }

        /// <inheritdoc />
        public int Next(int minValue, int maxValue) { return _random.Next(minValue, maxValue); }

        /// <inheritdoc />
        public double NextDouble() { return _random.NextDouble(); }
    }

}