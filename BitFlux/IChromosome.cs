namespace BitFlux
{
    using System;
    using BitFlux.Algorithms;

    public interface IChromosome<T, U> where U : IComparable<U>
    {
        int Length { get; }

        T[] Data { get; }

        U Fitness { get; }

        float Rank { get; }

        void InitializeRandom(Action<RandomGenerator, IChromosome<T, U>> initializationFunction, RandomGenerator generator);

        Tuple<IChromosome<T, U>, IChromosome<T, U>> Crossover(Func<RandomGenerator, IChromosome<T, U>, IChromosome<T, U>, Tuple<IChromosome<T, U>, IChromosome<T, U>>> crossoverFunction, IChromosome<T, U> other, RandomGenerator generator);

        void Mutate(Action<RandomGenerator, IChromosome<T, U>> mutationFunction, RandomGenerator generator);

        U ComputeFitness(Func<IChromosome<T, U>, U> fitnessFunction);

        float ComputeRank(Func<IChromosome<T, U>, float> rankingFunction);
    }
}