using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace CosmosTableApiSamples.Model
{
    public class GenericAnswer : TableEntity
    {
        public GenericAnswer()
        {
            
        }
        public GenericAnswer(string market,string service_scenario)
        {
            this.PartitionKey = market;
            this.RowKey = service_scenario;
        }
        public int Total_Clicks { get; set; }
        public int Link1 { get; set; }
        public int Link2 { get; set; }
        public int Link3 { get; set; }
        public int Link4 { get; set; }
        public int Link5 { get; set; }
        public int Link6 { get; set; }
    }
}
