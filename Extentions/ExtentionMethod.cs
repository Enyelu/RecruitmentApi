using Microsoft.Extensions.DependencyInjection;
using RecruitmentInfrastructure.Data;

namespace RecruitmentApi.Extentions
{
    public static class ExtentionMethod
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            // Your other services and configuration
            string connectionString = "AccountEndpoint=https://localhost:8081;AccountKey=C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==;";
            string databaseId = "RecruitmentApiDb";
            string containerId = "RecruitmentApiContainer";

            services.AddSingleton(provider => new CosmosDbService(connectionString, databaseId, containerId));
        }
    }
}
