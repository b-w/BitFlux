namespace BitFlux
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class EugenicsLab<TChromosome, TGene, TFitness>
        where TChromosome : IChromosome<TGene, TFitness>
        where TFitness : IComparable<TFitness>
    {
        public EugenicsLab(EugenicsLabSettings<TGene, TFitness> settings)
        {
            Settings = settings;
        }

        public EugenicsLabSettings<TGene, TFitness> Settings { get; set; }

        public IChromosome<TGene, TFitness>[] CurrentPopulation { get; set; }

        public IEnumerable<GenerationInfo<TGene, TFitness>> Run()
        {
            var gen = 1UL;
            var rng = Settings.Generator;
            var initFunc = Settings.InitializationFunction;
            var fitFunc = Settings.FitnessFunction;
            var rankFunc = Settings.RankingFunction;
            var crossFunc = Settings.CrossoverFunction;
            var mutateFunc = Settings.MutationFunction;
            var stopFunc = Settings.StoppingFunction;
            var populationSorter = new Comparison<IChromosome<TGene, TFitness>>((x, y) => -x.Rank.CompareTo(y.Rank));

            //
            // 1. generate random population

            var currentPopulation = new IChromosome<TGene, TFitness>[Settings.PopulationSize];
            for (int i = 0; i < currentPopulation.Length; i++) {
                currentPopulation[i] = (TChromosome)Activator.CreateInstance(typeof(TChromosome), Settings.ChromosomeLength);
            }
            Parallel.ForEach(currentPopulation, x =>
                {
                    x.InitializeRandom(initFunc, rng);
                });

            do {
                var newPopulation = new IChromosome<TGene, TFitness>[currentPopulation.Length];

                //
                // 2. compute fitness and rank

                Parallel.ForEach(currentPopulation, x =>
                    {
                        x.ComputeFitness(fitFunc);
                        x.ComputeRank(rankFunc);
                    });

                Array.Sort(currentPopulation, populationSorter);

                //
                // 3. select chromosomes for elitism

                for (int i = 0; i < Settings.ElitismCount; i++) {
                    newPopulation[i] = currentPopulation[i];
                }

                //
                // 4. create new population

                var rankedPopulation = new List<Tuple<IChromosome<TGene, TFitness>, float>>(
                        currentPopulation.Select(x => Tuple.Create(x, x.Rank))
                    );
                for (int i = Settings.ElitismCount; i < currentPopulation.Length; i++) {
                    //
                    // 4a. select chromosomes for crossover

                    var p1 = rng.RandomElementWeighted(rankedPopulation);
                    var p2 = rng.RandomElementWeighted(rankedPopulation);

                    //
                    // 4b. perform crossover

                    var newChromosome = p1.Crossover(crossFunc, p2, rng);

                    //
                    // 4c. perform mutation

                    newChromosome.Mutate(mutateFunc, rng);

                    //
                    // 4d. add to new population

                    newPopulation[i] = newChromosome;
                }

                //
                // 5. replace old population

                yield return new GenerationInfo<TGene, TFitness>(gen, currentPopulation);

                CurrentPopulation = currentPopulation;
                currentPopulation = newPopulation;
                gen++;
            } while (!stopFunc(gen, currentPopulation));
        }
    }
}