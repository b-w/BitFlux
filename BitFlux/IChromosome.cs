namespace BitFlux
{
    using System;
    using BitFlux.Algorithms;

    public interface IChromosome<TGene, TFitness> : IComparable<IChromosome<TGene, TFitness>>, ICloneable
        where TFitness : IComparable<TFitness>
    {
        int Length { get; }

        TGene[] Data { get; }

        TFitness Fitness { get; }

        float Rank { get; }

        void InitializeRandom(Action<RandomGenerator, IChromosome<TGene, TFitness>> initializationFunction, RandomGenerator generator);

        IChromosome<TGene, TFitness> Crossover(Func<RandomGenerator, IChromosome<TGene, TFitness>, IChromosome<TGene, TFitness>, IChromosome<TGene, TFitness>> crossoverFunction, IChromosome<TGene, TFitness> other, RandomGenerator generator);

        void Mutate(Action<RandomGenerator, IChromosome<TGene, TFitness>> mutationFunction, RandomGenerator generator);

        TFitness ComputeFitness(Func<IChromosome<TGene, TFitness>, TFitness> fitnessFunction);

        float ComputeRank(Func<IChromosome<TGene, TFitness>, float> rankingFunction);
    }
}