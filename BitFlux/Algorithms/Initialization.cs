namespace BitFlux.Algorithms
{
    using System;
    using System.Collections.Generic;

    public static class Initialization<TGene, TFitness> where TFitness : IComparable<TFitness>
    {
        public static Action<RandomGenerator, IChromosome<TGene, TFitness>> GetRandomOrderInitializationFunction(List<TGene> items)
        {
            return (rng, c) =>
                {
                    var itemsCopy = new List<TGene>(items);
                    for (int i = 0; i < c.Length; i++) {
                        var j = rng.NextInt(0, itemsCopy.Count);
                        c.Data[i] = itemsCopy[j];
                        itemsCopy.RemoveAt(j);
                    }
                };
        }

        public static Action<RandomGenerator, IChromosome<bool, TFitness>> GetBooleanInitializationFunction(float probability)
        {
            return (rng, c) =>
                {
                    for (int i = 0; i < c.Length; i++) {
                        c.Data[i] = rng.NextBool(probability);
                    }
                };
        }

        public static Action<RandomGenerator, IChromosome<int, TFitness>> GetIntegerInitializationFunction(int min, int max)
        {
            return (rng, c) =>
                {
                    for (int i = 0; i < c.Length; i++) {
                        c.Data[i] = rng.NextInt(min, max);
                    }
                };
        }

        public static Action<RandomGenerator, IChromosome<double, TFitness>> GetDoubleInitializationFunction(double min, double max)
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