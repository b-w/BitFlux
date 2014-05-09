namespace BitFlux
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using BitFlux.Algorithms;

    public class EugenicsLab<T, U, V>
        where T : IChromosome<U, V>
        where V : IComparable<V>
    {
        public EugenicsLab(EugenicsLabSettings<T, U, V> settings)
        {
            Settings = settings;
        }

        public EugenicsLabSettings<T, U, V> Settings { get; set; }

        public IChromosome<U, V>[] CurrentPopulation { get; set; }

        public IEnumerable<GenerationInfo<T, U, V>> Run()
        {
            var gen = 1UL;
            var rng = Settings.Generator;
            var initFunc = Settings.InitializationFunction as Action<RandomGenerator, IChromosome<U, V>>;
            var fitFunc = Settings.FitnessFunction as Func<IChromosome<U, V>, V>;
            var rankFunc = Settings.RankingFunction as Func<IChromosome<U, V>, float>;
            var crossFunc = Settings.CrossoverFunction as Func<RandomGenerator, IChromosome<U, V>, IChromosome<U, V>, IChromosome<U, V>>;
            var mutateFunc = Settings.MutationFunction as Action<RandomGenerator, IChromosome<U, V>>;
            var stopFunc = Settings.StoppingFunction as Func<ulong, IChromosome<U, V>[], bool>;
            var populationSorter = new Comparison<IChromosome<U, V>>((x, y) => { return -x.Rank.CompareTo(y.Rank); });

            //
            // 1. generate random population

            var currentPopulation = new IChromosome<U, V>[Settings.PopulationSize];
            for (int i = 0; i < currentPopulation.Length; i++) {
                currentPopulation[i] = (T)Activator.CreateInstance(typeof(T), Settings.ChromosomeLength);
            }
            Parallel.ForEach(currentPopulation, x =>
                {
                    x.InitializeRandom(initFunc, rng);
                });

            do {
                var newPopulation = new IChromosome<U, V>[currentPopulation.Length];

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

                var rankedPopulation = new List<Tuple<IChromosome<U, V>, float>>(
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

                yield return new GenerationInfo<T, U, V>(gen, currentPopulation);

                CurrentPopulation = currentPopulation;
                currentPopulation = newPopulation;
                gen++;
            } while (!stopFunc(gen, currentPopulation));
        }
    }
}