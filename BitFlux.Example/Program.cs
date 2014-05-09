namespace BitFlux.Example
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using BitFlux;
    using BitFlux.Algorithms;

    internal class Program
    {
        private static void Main(string[] args)
        {
            Example1();
            Console.ReadKey();
        }

        private static void Example1()
        {
            //
            // goal: find a list of 32 booleans with the most 'true' occurrences

            var settings = new EugenicsLabSettings<Chromosome<bool, int>, bool, int>();
            settings.ChromosomeLength = 32;
            settings.PopulationSize = 24;
            settings.ElitismCount = 4;
            settings.Generator = new RandomGenerator();

            settings.FitnessFunction = (c) =>
                {
                    return c.Data.Count(x => x);
                };

            settings.RankingFunction = (c) =>
                {
                    return c.Fitness;
                };

            settings.InitializationFunction = Initialization<Chromosome<bool, int>, bool, int>.GetBooleanInitializationFunction(0.25f);

            settings.CrossoverFunction = Crossover<Chromosome<bool, int>, bool, int>.GetSinglePointCrossoverFunction(0.85f);

            settings.MutationFunction = Mutation<Chromosome<bool, int>, bool, int>.GetBooleanMutationFunction(0.65f, 1, 4);

            settings.StoppingFunction = (c, gen) =>
                {
                    if (c > 60) {
                        return true;
                    } else if (gen[0].Fitness == 0) {
                        return true;
                    }

                    return false;
                };

            var lab = new EugenicsLab<Chromosome<bool, int>, bool, int>(settings);

            Console.WriteLine("+---------------------------+");
            GenerationInfo<Chromosome<bool, int>, bool, int> lastGeneration = null;
            foreach (var generation in lab.Run()) {
                lastGeneration = generation;

                Console.WriteLine("| Generation:    {0,4}       |", generation.Generation);
                Console.WriteLine("| Best fitness:  {0,4}       |", generation.BestFitness);
                Console.WriteLine("| Worst fitness: {0,4}       |", generation.WorstFitness);
                Console.WriteLine("| Mean fitness:  {0,4}       |", generation.MeanFitness);
                Console.WriteLine("+---------------------------+");
            }

            Console.WriteLine("Best Solution:");
            Console.WriteLine(lab.CurrentPopulation[0]);
        }
    }
}