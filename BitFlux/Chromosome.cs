namespace BitFlux.Chromosomes
{
    using System;
    using BitFlux.Algorithms;

    public class Chromosome<T, U>
    {
        public Chromosome(int length)
        {
            Length = length;
            Data = new T[length];
            FitnessComputed = false;
        }

        public Chromosome(int length, Action<RandomGenerator, Chromosome<T, U>> initializationFunction, RandomGenerator generator)
            : this(length)
        {
            InitializeRandom(initializationFunction, generator);
        }

        public int Length { get; private set; }

        public T[] Data { get; private set; }

        private bool FitnessComputed { get; set; }

        private U FitnessValue { get; set; }

        public virtual void InitializeRandom(Action<RandomGenerator, Chromosome<T, U>> initializationFunction, RandomGenerator generator)
        {
            initializationFunction(generator, this);
        }

        public virtual Tuple<Chromosome<T, U>, Chromosome<T, U>> Crossover(Func<RandomGenerator, Chromosome<T, U>, Chromosome<T, U>, Tuple<Chromosome<T, U>, Chromosome<T, U>>> crossoverFunction, Chromosome<T, U> other, RandomGenerator generator)
        {
            return crossoverFunction(generator, this, other);
        }

        public virtual void Mutate(Action<RandomGenerator, Chromosome<T, U>> mutationFunction, RandomGenerator generator)
        {
            mutationFunction(generator, this);
        }

        public virtual U ComputeFitness(Func<Chromosome<T, U>, U> fitnessFunction)
        {
            if (!FitnessComputed) {
                FitnessValue = fitnessFunction(this);
            }

            return FitnessValue;
        }
    }
}