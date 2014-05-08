namespace BitFlux.Algorithms
{
    using System;
    using System.Collections.Generic;

    public static class Initialization<T, U, V>
        where T : IChromosome<U, V>
        where V : IComparable<V>
    {
        public static Action<RandomGenerator, T> GetRandomOrderInitializationFunction(List<U> items)
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
    }
}