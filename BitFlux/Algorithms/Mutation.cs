namespace BitFlux.Algorithms
{
    using System;

    public static class Mutation<TGene, TFitness> where TFitness : IComparable<TFitness>
    {
        public static Action<RandomGenerator, IChromosome<TGene, TFitness>> GetItemSwapMutationFunction(float probability)
        {
            return (rng, c) =>
                {
                    if (rng.NextBool(probability)) {
                        var i1 = rng.NextInt(0, c.Length);
                        var i2 = rng.NextInt(0, c.Length);

                        var tmp = c.Data[i1];
                        c.Data[i1] = c.Data[i2];
                        c.Data[i2] = tmp;
                    }
                };
        }

        public static Action<RandomGenerator, IChromosome<bool, TFitness>> GetBooleanMutationFunction(float probability, int minCount, int maxCount)
        {
            return (rng, c) =>
                {
                    if (rng.NextBool(probability)) {
                        var count = rng.NextInt(minCount, maxCount);
                        for (int i = 0; i < count; i++) {
                            var j = rng.NextInt(0, c.Length);
                            c.Data[j] = !c.Data[j];
                        }
                    }
                };
        }

        public static Action<RandomGenerator, IChromosome<int, TFitness>> GetIntegerMutationFunction(float probability, int minCount, int maxCount, int minDelta, int maxDelta)
        {
            return (rng, c) =>
                {
                    if (rng.NextBool(probability)) {
                        var count = rng.NextInt(minCount, maxCount);
                        for (int i = 0; i < count; i++) {
                            var j = rng.NextInt(0, c.Length);
                            var delta = rng.NextInt(minDelta, maxDelta);
                            c.Data[j] += delta;
                        }
                    }
                };
        }

        public static Action<RandomGenerator, IChromosome<double, TFitness>> GetDoubleMutationFunction(float probability, int minCount, int maxCount, double minDelta, double maxDelta)
        {
            return (rng, c) =>
                {
                    if (rng.NextBool(probability)) {
                        var count = rng.NextInt(minCount, maxCount);
                        for (int i = 0; i < count; i++) {
                            var j = rng.NextInt(0, c.Length);
                            var delta = rng.NextDouble(minDelta, maxDelta);
                            c.Data[j] += delta;
                        }
                    }
                };
        }
    }
}