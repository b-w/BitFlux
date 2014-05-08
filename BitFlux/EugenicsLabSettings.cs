namespace BitFlux
{
    using System;
    using BitFlux.Algorithms;

    public class EugenicsLabSettings<T, U, V>
        where T : IChromosome<U, V>
        where V : IComparable<V>
    {
        public int ChromosomeLength { get; set; }

        public int PopulationSize { get; set; }

        public int ElitismCount { get; set; }

        public RandomGenerator Generator { get; set; }

        public Action<RandomGenerator, T> InitializationFunction { get; set; }

        public Func<RandomGenerator, T, T, Tuple<T, T>> CrossoverFunction { get; set; }

        public Action<RandomGenerator, T> MutationFunction { get; set; }

        public Func<T, V> FitnessFunction { get; set; }

        public Func<T, float> RankingFunction { get; set; }

        public Func<ulong, T[], bool> StoppingFunction { get; set; }
    }
}