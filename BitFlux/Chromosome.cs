namespace BitFlux
{
    using System;
    using System.Linq;
    using BitFlux.Algorithms;

    public class Chromosome<T, U> : IChromosome<T, U>
        where U : IComparable<U>
    {
        public Chromosome(int length)
        {
            Length = length;
            Data = new T[length];
            FitnessComputed = false;
            RankComputed = false;
        }

        public Chromosome(T[] data)
            : this(data.Length)
        {
            data.CopyTo(Data, 0);
        }

        public Chromosome(int length, Action<RandomGenerator, IChromosome<T, U>> initializationFunction, RandomGenerator generator)
            : this(length)
        {
            InitializeRandom(initializationFunction, generator);
        }

        public int Length { get; private set; }

        public T[] Data { get; private set; }

        public U Fitness { get; private set; }

        public float Rank { get; private set; }

        private bool FitnessComputed { get; set; }

        private bool RankComputed { get; set; }

        public virtual void InitializeRandom(Action<RandomGenerator, IChromosome<T, U>> initializationFunction, RandomGenerator generator)
        {
            initializationFunction(generator, this);
        }

        public virtual IChromosome<T, U> Crossover(Func<RandomGenerator, IChromosome<T, U>, IChromosome<T, U>, IChromosome<T, U>> crossoverFunction, IChromosome<T, U> other, RandomGenerator generator)
        {
            return crossoverFunction(generator, this, other);
        }

        public virtual void Mutate(Action<RandomGenerator, IChromosome<T, U>> mutationFunction, RandomGenerator generator)
        {
            mutationFunction(generator, this);
        }

        public virtual U ComputeFitness(Func<IChromosome<T, U>, U> fitnessFunction)
        {
            if (!FitnessComputed) {
                Fitness = fitnessFunction(this);
            }

            return Fitness;
        }

        public virtual float ComputeRank(Func<IChromosome<T, U>, float> rankingFunction)
        {
            if (!RankComputed) {
                Rank = rankingFunction(this);
            }

            return Rank;
        }

        public int CompareTo(IChromosome<T, U> other)
        {
            return this.Fitness.CompareTo(other.Fitness);
        }

        public override string ToString()
        {
            return String.Format("[{0}]",
                String.Join(", ",
                    Data.Select(x => x.ToString())
                )
            );
        }
    }
}