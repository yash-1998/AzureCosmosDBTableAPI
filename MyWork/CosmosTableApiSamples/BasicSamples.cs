using System;
using System.Collections.Generic;
using System.Text;

namespace CosmosTableApiSamples
{
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.Azure.Cosmos.Table;
    using Model;

    public class BasicSamples
    {
        public async Task RunSamples()
        {
            Console.WriteLine("Azure Cosmos DB Table - CricketTry\n");
            Console.WriteLine();

            string tableName = "CricketTry" + Guid.NewGuid().ToString().Substring(0, 5);

            // Create or reference an existing table
            var table = await Common.CreateTableAsync(tableName);

            try
            {
                // Demonstrate basic CRUD functionality 
                var entities = ReadFile().ToList();

                var batchOperations = new List<TableBatchOperation>();
                var chunkSize = 100;
                var batch_id = 0;
                while (entities.Any())
                {
                    var batchOperation = new TableBatchOperation();
                    var chunk = entities?.Take(chunkSize).ToList();
                    chunk?.ForEach(entity =>
                    {
                        try
                        {
                            batchOperation.Insert(entity);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.StackTrace);
                        }
                    });
                    batchOperations.Add(batchOperation);
                    entities = entities.Skip(chunkSize).ToList();
                    Console.WriteLine("Creating batch " + batch_id);
                    batch_id++;
                }

                foreach (var batchOperation in batchOperations)
                {
                    await table.ExecuteBatchAsync(batchOperation);
                }
                Console.WriteLine("Done Insertion");
            }
            catch (StorageException e)
            {
                Console.WriteLine(e.StackTrace);
                Console.ReadLine();
                throw;
            }

            finally
            {
                // Delete the table
                // await table.DeleteIfExistsAsync();
            }
        }

        private List<Answer> ReadFile()
        {
            var entitiesToInsert = new List<Answer>();
            var impressions = File.ReadAllLines("C:\\Users\\T-YAGUPT\\Desktop\\out.csv");

            foreach (var impression in impressions)
            {
                var tokenized = impression.Split(',');
                try
                {
                    var entity = new Answer(tokenized[0], tokenized[1])
                    {
                        Datetime = Convert.ToDateTime(tokenized[2]),
                        Market = tokenized[3],
                        Total_clicks = Convert.ToInt32(tokenized[4]),
                        Match1_clicks = Convert.ToInt32(tokenized[5]),
                        Match2_clicks = Convert.ToInt32(tokenized[6]),
                        Match3_clicks = Convert.ToInt32(tokenized[7]),
                        Match4_clicks = Convert.ToInt32(tokenized[8]),
                        Match5_clicks = Convert.ToInt32(tokenized[9]),
                        Match6_clicks = Convert.ToInt32(tokenized[10]),
                    };
                    entitiesToInsert.Add(entity);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }
            }
            return entitiesToInsert;
        }

        //private List<TableBatchOperation> GetTableBatchOperations(IGrouping<string, ITableEntity> group)
        //{
        //    var operationsList = new List<TableBatchOperation>();
        //    var entities = group.ToList();
        //    var chunkSize = 100;

        //    while (entities.Any())
        //    {
        //        TableBatchOperation batchOperation = new TableBatchOperation();
        //        var chunk = entities.Take(chunkSize).ToList();
        //        chunk.ForEach(entity =>
        //        {
        //            try
        //            {
        //                batchOperation.Insert(entity);
        //            }
        //            catch { }
        //        });
        //        operationsList.Add(batchOperation);
        //        entities = entities.Skip(chunkSize).ToList();
        //    }
        //    return operationsList;
        //}
        //private static async Task BasicDataOperationsAsync(CloudTable table)
        //{
        //    //////// create an instance of a customer entity. see the model\customerentity.cs for a description of the entity.
        //    singleentity entity = new singleentity("cricketanswerv2", "en-us")
        //    {
        //       rawquery = "cricket world cup"
        //    };

        //    //////// demonstrate how to insert the entity
        //    console.writeline("insert an entity.");
        //    entity = await samplesutils.insertormergeentityasync(table, entity);

        //    //////// demonstrate how to update the entity by changing the phone number
        //    console.writeline("update an existing entity using the insertormerge upsert operation.");
        //    entity.rawquery = "icc worldcup";
        //    await samplesutils.insertormergeentityasync(table, entity);
        //    console.writeline();

        //    //////// demonstrate how to read the updated entity using a point query 
        //    console.writeline("reading the updated entity.");
        //    entity = await samplesutils.retrieveentityusingpointqueryasync(table, "cricketanswerv2", "en-us");
        //    console.writeline(entity.rawquery);
        //}
    }
}



// <summary>
/// Demonstrate inserting of a large batch of entities. Some considerations for batch operations:
///  1. You can perform updates, deletes, and inserts in the same single batch operation.
///  2. A single batch operation can include up to 100 entities.
///  3. All entities in a single batch operation must have the same partition key.
///  4. While it is possible to perform a query as a batch operation, it must be the only operation in the batch.
///  5. Batch size must be less than or equal to 2 MB
/// </summary>
/// <param name="table">Sample table name</param>
/// <param name="partitionKey">The partition for the entity</param>
/// <returns>A Task object</returns>
//private static async Task BatchInsertOfCustomerEntitiesAsync(CloudTable table, string partitionKey)
//{
//    try
//    {
//        // Create the batch operation. 
//        TableBatchOperation batchOperation = new TableBatchOperation();

//        // The following code  generates test data for use during the query samples.  
//        for (int i = 0; i < 100; i++)
//        {
//            batchOperation.InsertOrMerge(new CustomerEntity(partitionKey, string.Format("{0}", i.ToString("D4")))
//            {
//                Email = string.Format("{0}@contoso.com", i.ToString("D4")),
//                PhoneNumber = string.Format("425-555-{0}", i.ToString("D4"))
//            });
//        }

//        // Execute the batch operation.
//        TableBatchResult results = await table.ExecuteBatchAsync(batchOperation);
//        foreach (var res in results)
//        {
//            var customerInserted = res.Result as CustomerEntity;
//            Console.WriteLine("Inserted entity with\t Etag = {0} and PartitionKey = {1}, RowKey = {2}", customerInserted.ETag, customerInserted.PartitionKey, customerInserted.RowKey);
//        }

//        if (results.RequestCharge.HasValue)
//        {
//            Console.WriteLine("Request Charge of the Batch Operation against Cosmos DB Table: " + results.RequestCharge);
//        }
//    }
//    catch (StorageException e)
//    {
//        Console.WriteLine(e.Message);
//        Console.ReadLine();
//        throw;
//    }
//}