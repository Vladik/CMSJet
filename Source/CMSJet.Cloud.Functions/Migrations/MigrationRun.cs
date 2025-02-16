using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace CMSJet.Cloud.Functions.Migrations
{
    public class MigrationRun
    {
        private readonly ILogger<MigrationRun> _logger;

        public MigrationRun(ILogger<MigrationRun> logger)
        {
            _logger = logger;
        }

        [Function("MigrationRun")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "migration-run")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            return new OkObjectResult("Welcome to Azure Functions!");
        }
    }
}
