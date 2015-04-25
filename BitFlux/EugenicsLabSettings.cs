namespace BitFlux
{
    using System;
    using BitFlux.Algorithms;

    public class EugenicsLabSettings<TGene, TFitness> where TFitness : IComparable<TFitness>
    {
        public int ChromosomeLength { get; set; }

        public int PopulationSize { get; set; }

        public int ElitismCount { get; set; }

        public RandomGenerator Generator { get; set; }

        public Action<RandomGenerator, IChromosome<TGene, TFitness>> InitializationFunction { get; set; }

        public Func<RandomGenerator, IChromosome<TGene, TFitness>, IChromosome<TGene, TFitness>, IChromosome<TGene, TFitness>> CrossoverFunction { get; set; }

        public Action<RandomGenerator, IChromosome<TGene, TFitness>> MutationFunction { get; set; }

        public Func<IChromosome<TGene, TFitness>, TFitness> FitnessFunction { get; set; }

        public Func<IChromosome<TGene, TFitness>, float> RankingFunction { get; set; }

        public Func<ulong, IChromosome<TGene, TFitness>[], bool> StoppingFunction { get; set; }
    }
}