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
    using Microsoft.Extensions.Configuration;
    using Model;

    public class Dbhelper
    {

        public Dbhelper()
        {

        }
        public async Task InsertInTable()
        {
            string tableName = "GenericTable";

            // Create or reference an existing table
            var table = await Common.CreateTableAsync(tableName);
            try
            {
                //Demonstrate basic CRUD functionality
                var entities = ReadFile().ToList();
                
                foreach(var entity in entities)
                {
                    await SamplesUtils.InsertOrMergeEntityAsync(table, entity);
                }
                Console.WriteLine("Done Insertion");
            }
            catch (StorageException e)
            {
                Console.WriteLine(e.StackTrace);
                Console.ReadLine();
                throw;
            }
        }

        public async Task FetchData(CloudTable table,String market,String service_scenario)
        {
            TableQuery<GenericAnswer> query = new TableQuery<GenericAnswer>()
                .Where(
                    TableQuery.CombineFilters(
                        TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal,market),
                        TableOperators.And,
                        TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, service_scenario)
                ));

            var result = await table.ExecuteQuerySegmentedAsync<GenericAnswer>(query, null);
            Console.WriteLine(result);

        }
        private List<GenericAnswer> ReadFile()
        {
            var entitiesToInsert = new List<GenericAnswer>();
            var lines = File.ReadAllLines("C:\\Users\\T-YAGUPT\\Desktop\\CricketAnswerV2_CricketAnswerV2_FLIGHT_prefetching_2019-06-08_.csv");

            foreach (var line in lines)
            {
                var tokenized = line.Split(',');
                try
                {
                    Console.WriteLine(tokenized[1]);
                    var entity = new GenericAnswer(tokenized[1],tokenized[0])
                    {
                        Total_Clicks = Convert.ToInt32(tokenized[2]),
                        Link1 = Convert.ToInt32(tokenized[3]),
                        Link2 = Convert.ToInt32(tokenized[4]),
                        Link3 = Convert.ToInt32(tokenized[5]),
                        Link4 = Convert.ToInt32(tokenized[6]),
                        Link5 = Convert.ToInt32(tokenized[7]),
                        Link6 = Convert.ToInt32(tokenized[8]),
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