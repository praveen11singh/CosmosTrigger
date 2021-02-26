using System;
using System.Collections.Generic;
using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace CosmosTrigger
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static void Run([CosmosDBTrigger(
            databaseName: "arenadbid",
            collectionName: "arenacontainerid",
            ConnectionStringSetting = "mydbconnection",
            LeaseCollectionName = "leases")]IReadOnlyList<Document> input,
           [Queue("arenaqueue"), StorageAccount("AzureWebJobsStorage")] ICollector<string> msg
            , ILogger log)
        {
            if (input != null && input.Count > 0)
            {
                msg.Add($"name pass is {input[0].Id}");
                log.LogInformation("Documents modified " + input.Count);
                log.LogInformation("First document Id " + input[0].Id);
            }
        }
    }
}
