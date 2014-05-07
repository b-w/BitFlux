namespace BitFlux.Algorithms
{
    using System;

    public class RandomGenerator
    {
        private readonly Random m_random;

        public RandomGenerator()
        {
            m_random = new Random();
        }

        public virtual int Next(int min, int max)
        {
            return m_random.Next(min, max);
        }
    }
}