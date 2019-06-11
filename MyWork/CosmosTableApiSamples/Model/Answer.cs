using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CosmosTableApiSamples.Model
{
    public class Answer : TableEntity
    {
        public Answer(string partitionKey, string rowKey)
        {
            this.PartitionKey = partitionKey;
            this.RowKey = rowKey;
        }
        public DateTime Datetime { get; set; }
        public string Market { get; set; }
        public int Total_clicks { get; set; }
        public int Match1_clicks { get; set; }
        public int Match2_clicks { get; set; }
        public int Match3_clicks { get; set; }
        public int Match4_clicks { get; set; }
        public int Match5_clicks { get; set; }
        public int Match6_clicks { get; set; }
    }
}
