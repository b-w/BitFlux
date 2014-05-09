namespace BitFlux
{
    using System;

    public class GenerationInfo<T, U, V>
        where T : IChromosome<U, V>
        where V : IComparable<V>
    {
        public GenerationInfo(ulong generation, IChromosome<U, V>[] population)
        {
            Generation = generation;
            BestFitness = population[0].Fitness;
            WorstFitness = population[population.Length - 1].Fitness;
            MeanFitness = population[population.Length / 2].Fitness;
        }

        public ulong Generation { get; set; }

        public V BestFitness { get; set; }

        public V WorstFitness { get; set; }

        public V MeanFitness { get; set; }
    }
}