namespace BitFlux
{
    using System;

    public class GenerationInfo<TGene, TFitness> where TFitness : IComparable<TFitness>
    {
        public GenerationInfo(ulong generation, IChromosome<TGene, TFitness>[] population)
        {
            Generation = generation;
            BestFitness = population[0].Fitness;
            WorstFitness = population[population.Length - 1].Fitness;
            MeanFitness = population[population.Length / 2].Fitness;
        }

        public ulong Generation { get; set; }

        public TFitness BestFitness { get; set; }

        public TFitness WorstFitness { get; set; }

        public TFitness MeanFitness { get; set; }
    }
}