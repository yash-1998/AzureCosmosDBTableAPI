using System;
using System.Collections.Generic;
using System.Text;

namespace CosmosTableApiSamples
{
    using System.Threading.Tasks;
    using Microsoft.Azure.Cosmos.Table;
    using Model;

    public class BasicSamples
    {
        public async Task RunSamples()
        {
            Console.WriteLine("Azure Cosmos DB Table - Basic Samples\n");
            Console.WriteLine();

            string tableName = "demo" + Guid.NewGuid().ToString().Substring(0, 5);

            // Create or reference an existing table
            CloudTable table = await Common.CreateTableAsync(tableName);

            try
            {
                // Demonstrate basic CRUD functionality 
                await BasicDataOperationsAsync(table);
            }
            finally
            {
                // Delete the table
                // await table.DeleteIfExistsAsync();
            }
        }

        private static async Task BasicDataOperationsAsync(CloudTable table)
        {
            // Create an instance of a customer entity. See the Model\CustomerEntity.cs for a description of the entity.
            SingleEntity entity = new SingleEntity("CricketAnswerV2", "en-us")
            {
                RawQuery = "Cricket World Cup"
            };

            // Demonstrate how to insert the entity
            Console.WriteLine("Insert an Entity.");
            entity = await SamplesUtils.InsertOrMergeEntityAsync(table, entity);

            // Demonstrate how to Update the entity by changing the phone number
            Console.WriteLine("Update an existing Entity using the InsertOrMerge Upsert Operation.");
            entity.RawQuery = "ICC WORLDCUP";
            await SamplesUtils.InsertOrMergeEntityAsync(table, entity);
            Console.WriteLine();

            // Demonstrate how to Read the updated entity using a point query 
            Console.WriteLine("Reading the updated Entity.");
            entity = await SamplesUtils.RetrieveEntityUsingPointQueryAsync(table, "CricketAnswerV2", "en-us");
        //    Console.WriteLine(entity.RawQuery);

            // Demonstrate how to Delete an entity
            //Console.WriteLine("Delete the entity. ");
            //await SamplesUtils.DeleteEntityAsync(table, customer);
            //Console.WriteLine();
        }

    }
}
