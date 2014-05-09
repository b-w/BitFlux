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

        public Action<RandomGenerator, IChromosome<U, V>> InitializationFunction { get; set; }

        public Func<RandomGenerator, IChromosome<U, V>, IChromosome<U, V>, IChromosome<U, V>> CrossoverFunction { get; set; }

        public Action<RandomGenerator, IChromosome<U, V>> MutationFunction { get; set; }

        public Func<IChromosome<U, V>, V> FitnessFunction { get; set; }

        public Func<IChromosome<U, V>, float> RankingFunction { get; set; }

        public Func<ulong, IChromosome<U, V>[], bool> StoppingFunction { get; set; }
    }
}