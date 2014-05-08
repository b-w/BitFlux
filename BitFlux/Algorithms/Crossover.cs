namespace BitFlux.Algorithms
{
    using System;

    public static class Crossover<T, U, V>
        where T : Chromosome<U, V>
        where V : IComparable<V>
    {
        public static Func<RandomGenerator, T, T, Tuple<T, T>> GetSinglePointCrossoverFunction(float probability)
        {
            return (rng, c1, c2) =>
                {
                    if (rng.NextBoolWeighted(probability)) {
                        var length = c1.Length;
                        var d1 = new U[length];
                        var d2 = new U[length];
                        var crossoverPoint = rng.NextInt(0, length);

                        Array.Copy(c1.Data, 0, d1, 0, crossoverPoint);
                        Array.Copy(c2.Data, crossoverPoint, d1, crossoverPoint, length - crossoverPoint);

                        Array.Copy(c1.Data, crossoverPoint, d2, crossoverPoint, length - crossoverPoint);
                        Array.Copy(c2.Data, 0, d2, 0, crossoverPoint);

                        var n1 = (T)Activator.CreateInstance(typeof(T), d1);
                        var n2 = (T)Activator.CreateInstance(typeof(T), d2);

                        return Tuple.Create(n1, n2);
                    } else {
                        return Tuple.Create(c1, c2);
                    }
                };
        }
    }
}