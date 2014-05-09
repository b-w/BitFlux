namespace BitFlux.Algorithms
{
    using System;

    public static class Crossover<T, U, V>
        where T : IChromosome<U, V>
        where V : IComparable<V>
    {
        public static Func<RandomGenerator, IChromosome<U, V>, IChromosome<U, V>, IChromosome<U, V>> GetSinglePointCrossoverFunction(float probability)
        {
            return (rng, c1, c2) =>
                {
                    if (rng.NextBool(probability)) {
                        var length = c1.Length;
                        var data = new U[length];
                        var crossoverPoint = rng.NextInt(0, length);

                        Array.Copy(c1.Data, 0, data, 0, crossoverPoint);
                        Array.Copy(c2.Data, crossoverPoint, data, crossoverPoint, length - crossoverPoint);

                        return (T)Activator.CreateInstance(c1.GetType(), data);
                    } else {
                        if (rng.NextBool()) {
                            return c1;
                        } else {
                            return c2;
                        }
                    }
                };
        }
    }
}