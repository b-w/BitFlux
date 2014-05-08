namespace BitFlux.Algorithms
{
    using System;

    public static class Mutation<T, U, V>
        where T : Chromosome<U, V>
        where V : IComparable<V>
    {
        public static Action<RandomGenerator, T> GetItemSwapMutationFunction(float probability)
        {
            return (rng, c) =>
                {
                    if (rng.NextBoolWeighted(probability)) {
                        var i1 = rng.NextInt(0, c.Length);
                        var i2 = rng.NextInt(0, c.Length);
                        U tmp;

                        tmp = c.Data[i1];
                        c.Data[i1] = c.Data[i2];
                        c.Data[i2] = tmp;
                    }
                };
        }
    }
}