namespace BitFlux.Algorithms
{
    using System;
    using System.Collections.Generic;

    public static class Initialization<T, U, V>
        where T : IChromosome<U, V>
        where V : IComparable<V>
    {
        public static Action<RandomGenerator, IChromosome<U, V>> GetRandomOrderInitializationFunction(List<U> items)
        {
            return (rng, c) =>
                {
                    var itemsCopy = new List<U>(items);
                    for (int i = 0; i < c.Length; i++) {
                        var j = rng.NextInt(0, itemsCopy.Count);
                        c.Data[i] = itemsCopy[j];
                        itemsCopy.RemoveAt(j);
                    }
                };
        }

        public static Action<RandomGenerator, IChromosome<bool, V>> GetBooleanInitializationFunction(float probability)
        {
            return (rng, c) =>
                {
                    for (int i = 0; i < c.Length; i++) {
                        c.Data[i] = rng.NextBool(probability);
                    }
                };
        }

        public static Action<RandomGenerator, IChromosome<int, V>> GetIntegerInitializationFunction(int min, int max)
        {
            return (rng, c) =>
                {
                    for (int i = 0; i < c.Length; i++) {
                        c.Data[i] = rng.NextInt(min, max);
                    }
                };
        }

        public static Action<RandomGenerator, IChromosome<double, V>> GetDoubleInitializationFunction(double min, double max)
        {
            return (rng, c) =>
                {
                    for (int i = 0; i < c.Length; i++) {
                        c.Data[i] = rng.NextDouble(min, max);
                    }
                };
        }
    }
}