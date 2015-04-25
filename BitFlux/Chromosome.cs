namespace BitFlux
{
    using System;
    using System.Linq;
    using BitFlux.Algorithms;

    public class Chromosome<TGene, TFitness> : IChromosome<TGene, TFitness>
        where TFitness : IComparable<TFitness>
    {
        public Chromosome(int length)
        {
            Length = length;
            Data = new TGene[length];
            FitnessComputed = false;
            RankComputed = false;
        }

        public Chromosome(TGene[] data)
            : this(data.Length)
        {
            data.CopyTo(Data, 0);
        }

        public Chromosome(int length, Action<RandomGenerator, IChromosome<TGene, TFitness>> initializationFunction, RandomGenerator generator)
            : this(length)
        {
            InitializeRandom(initializationFunction, generator);
        }

        public int Length { get; private set; }

        public TGene[] Data { get; private set; }

        public TFitness Fitness { get; private set; }

        public float Rank { get; private set; }

        private bool FitnessComputed { get; set; }

        private bool RankComputed { get; set; }

        public void InitializeRandom(Action<RandomGenerator, IChromosome<TGene, TFitness>> initializationFunction, RandomGenerator generator)
        {
            initializationFunction(generator, this);
        }

        public virtual IChromosome<TGene, TFitness> Crossover(Func<RandomGenerator, IChromosome<TGene, TFitness>, IChromosome<TGene, TFitness>, IChromosome<TGene, TFitness>> crossoverFunction, IChromosome<TGene, TFitness> other, RandomGenerator generator)
        {
            return crossoverFunction(generator, this, other);
        }

        public virtual void Mutate(Action<RandomGenerator, IChromosome<TGene, TFitness>> mutationFunction, RandomGenerator generator)
        {
            mutationFunction(generator, this);
        }

        public virtual TFitness ComputeFitness(Func<IChromosome<TGene, TFitness>, TFitness> fitnessFunction)
        {
            if (!FitnessComputed) {
                Fitness = fitnessFunction(this);
            }

            return Fitness;
        }

        public virtual float ComputeRank(Func<IChromosome<TGene, TFitness>, float> rankingFunction)
        {
            if (!RankComputed) {
                Rank = rankingFunction(this);
            }

            return Rank;
        }

        public int CompareTo(IChromosome<TGene, TFitness> other)
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

        public object Clone()
        {
            return new Chromosome<TGene, TFitness>(Data);
        }
    }
}