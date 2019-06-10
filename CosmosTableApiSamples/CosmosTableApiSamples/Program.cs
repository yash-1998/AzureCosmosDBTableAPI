using System;

namespace CosmosTableApiSamples
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine("Azure Cosmos Table Samples");
            BasicSamples basicSamples = new BasicSamples();
            basicSamples.RunSamples().Wait();
            Console.WriteLine();
            Console.WriteLine("Press any key to exit");
            Console.Read();

        }
    }
}
