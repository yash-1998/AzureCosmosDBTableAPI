using System;

namespace CosmosTableApiSamples
{
    
    class Program
    {
        static void Main(string[] args)
        {
            Dbhelper helper = new Dbhelper();
            //helper.InsertInTable().Wait();
            var table = await Common.CreateTableAsync(tableName);
            helper.FetchData();
            Console.WriteLine();
            Console.WriteLine("Press any key to exit");
            Console.Read();
        }
    }
}
