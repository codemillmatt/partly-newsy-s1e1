using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PartlyNewsy.Models;

namespace PartlyNewsy.Functions
{
    public static class GetTopNews
    {
        [FunctionName("GetTopNews")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            var article = new Article
            {
                Headline = "News!",
                Publisher = "Matt"
            };

            return new OkObjectResult(article);
        }
    }
}
