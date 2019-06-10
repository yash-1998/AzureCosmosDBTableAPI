using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace CosmosTableApiSamples.Model
{
    public class SingleEntity : TableEntity
    {
        public SingleEntity()
        {
        }

        public SingleEntity(string ServiceName,string Market)
        {
            PartitionKey = ServiceName;
            RowKey = Market;
        }
        public string RawQuery { get; set; }
    }
}
