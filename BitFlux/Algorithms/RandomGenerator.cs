namespace BitFlux.Algorithms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class RandomGenerator
    {
        private readonly Random m_random;

        public RandomGenerator()
        {
            m_random = new Random();
        }

        public virtual bool NextBool()
        {
            return m_random.Next(0, 2) == 0;
        }

        public virtual bool NextBool(float probability)
        {
            if (probability <= 0) {
                return false;
            } else if (probability >= 1f) {
                return true;
            }

            return m_random.NextDouble() < probability;
        }

        public virtual int NextInt(int min, int max)
        {
            return m_random.Next(min, max);
        }

        public virtual double NextDouble()
        {
            return m_random.NextDouble();
        }

        public virtual double NextDouble(double min, double max)
        {
            return min + (m_random.NextDouble() * (max - min));
        }

        public virtual T RandomElementWeighted<T>(IEnumerable<Tuple<T, float>> collection)
        {
            var totalWeight = collection.Sum(x => x.Item2);
            var target = m_random.NextDouble() * totalWeight;
            var current = 0f;

            foreach (var item in collection) {
                current += item.Item2;
                if (current >= target) {
                    return item.Item1;
                }
            }

            return default(T);
        }
    }
}