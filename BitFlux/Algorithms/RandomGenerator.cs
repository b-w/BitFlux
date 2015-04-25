namespace BitFlux.Algorithms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class RandomGenerator
    {
        private readonly Random _random;

        public RandomGenerator()
        {
            _random = new Random();
        }

        public virtual bool NextBool()
        {
            return _random.Next(0, 2) == 0;
        }

        public virtual bool NextBool(float probability)
        {
            if (probability <= 0) {
                return false;
            }
            if (probability >= 1f) {
                return true;
            }

            return _random.NextDouble() < probability;
        }

        public virtual int NextInt(int min, int max)
        {
            return _random.Next(min, max);
        }

        public virtual double NextDouble()
        {
            return _random.NextDouble();
        }

        public virtual double NextDouble(double min, double max)
        {
            return min + (_random.NextDouble() * (max - min));
        }

        public virtual T RandomElementWeighted<T>(ICollection<Tuple<T, float>> collection)
        {
            var totalWeight = collection.Sum(x => x.Item2);
            var target = _random.NextDouble() * totalWeight;
            var current = 0f;

            foreach (var item in collection) {
                current += item.Item2;
                if (current >= target) {
                    return item.Item1;
                }
            }

            return collection.First().Item1;
        }
    }
}