namespace BitFlux.Example
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using BitFlux;
    using BitFlux.Algorithms;

    internal class Program
    {
        private static void Main(string[] args)
        {
            var someList = Enumerable.Range(1, 10).ToList();

            var settings = new EugenicsLabSettings<Chromosome<int, int>, int, int>()
                {
                    ChromosomeLength = 10,
                    PopulationSize = 30,
                    Generator = new RandomGenerator(),
                    InitializationFunction = Initialization<IChromosome<int, int>, int, int>.GetRandomOrderInitializationFunction(someList)
                };

            var lab = new EugenicsLab<Chromosome<int, int>, int, int>(settings);

            lab.Run();
        }
    }
}