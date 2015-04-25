namespace BitFlux.Algorithms
{
    using System;

    public static class Crossover<TGene, TFitness> where TFitness : IComparable<TFitness>
    {
        public static Func<RandomGenerator, IChromosome<TGene, TFitness>, IChromosome<TGene, TFitness>, IChromosome<TGene, TFitness>> GetSinglePointCrossoverFunction(float probability)
        {
            return (rng, c1, c2) =>
                {
                    if (rng.NextBool(probability)) {
                        var length = c1.Length;
                        var data = new TGene[length];
                        var crossoverPoint = rng.NextInt(0, length);

                        Array.Copy(c1.Data, 0, data, 0, crossoverPoint);
                        Array.Copy(c2.Data, crossoverPoint, data, crossoverPoint, length - crossoverPoint);

                        return (IChromosome<TGene, TFitness>)Activator.CreateInstance(c1.GetType(), data);
                    }

                    if (rng.NextBool()) {
                        return c1;
                    }
                    return c2;
                };
        }

        public static Func<RandomGenerator, IChromosome<TGene, TFitness>, IChromosome<TGene, TFitness>, IChromosome<TGene, TFitness>> GetRandomSelectionCrossoverFunction(float probability)
        {
            return (rng, c1, c2) =>
                {
                    if (rng.NextBool(probability)) {
                        var length = c1.Length;
                        var data = new TGene[length];

                        for (int i = 0; i < length; i++) {
                            if (rng.NextBool()) {
                                data[i] = c1.Data[i];
                            } else {
                                data[i] = c2.Data[i];
                            }
                        }

                        return (IChromosome<TGene, TFitness>)Activator.CreateInstance(c1.GetType(), data);
                    }

                    if (rng.NextBool()) {
                        return c1;
                    }
                    return c2;
                };
        }
    }
}