namespace BitFlux
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
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

        public T Run()
        {
            var gen = 1UL;
            var initFunc = Settings.InitializationFunction as Action<RandomGenerator, IChromosome<U, V>>;
            var stopFunc = Settings.StoppingFunction as Func<ulong, IChromosome<U, V>[], bool>;

            //
            // 1. generate random population

            var currentPopulation = new T[Settings.PopulationSize];
            for (int i = 0; i < currentPopulation.Length; i++) {
                currentPopulation[i] = (T)Activator.CreateInstance(typeof(T), Settings.ChromosomeLength);
            }
            Parallel.ForEach(currentPopulation, x =>
                {
                    x.InitializeRandom(initFunc, Settings.Generator);
                });

            throw new NotImplementedException("not quite there yet...");
        }
    }
}