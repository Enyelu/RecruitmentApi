using Microsoft.Azure.Cosmos;
using RecruitmentInfrastructure.Data.Interface;

namespace RecruitmentInfrastructure.Data
{
    public class CosmosDbService : ICosmoDbService
    {
        private readonly CosmosClient _cosmosClient;
        private readonly Container _container;

        const string _connectionString = "AccountEndpoint=https://localhost:8081;AccountKey=C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==;";
        const string _databaseId = "RecruitmentApiDb";
        const string _containerId = "RecruitmentApiContainer";

        public CosmosDbService()
        {
            _cosmosClient = new CosmosClient(_connectionString);
           
            _container = _cosmosClient.GetContainer(_databaseId, _containerId);
        }

        public async Task InitializeAsync()
        {
            // Create or get the database
            Database database = await _cosmosClient.CreateDatabaseIfNotExistsAsync(_databaseId);

            // Create or get the container with a partition key path
            await database.CreateContainerIfNotExistsAsync(_containerId, "/PartitionKeyPath");
        }

        public async Task SaveAsync<T>(T item)
        {
            await _container.CreateItemAsync(item);
        }

        public async Task SaveManyAsync<T>(IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                await _container.CreateItemAsync(item);
            }
        }
        
        public async Task UpdateAsync<T>(T updatedItem)
        {
            await _container.UpsertItemAsync(updatedItem);
        }
       
        public async Task<IEnumerable<T>> GetManyAsync<T>(string query)
        {
            var resultSet = _container.GetItemQueryIterator<T>(new QueryDefinition(query));
            var items = new List<T>();

            while (resultSet.HasMoreResults)
            {
                    var response = await resultSet.ReadNextAsync();
                    items.AddRange(response);
            }
            return items;
        }

        public async Task DeleteAsync<T>(string itemId, string partitionKey)
        {
            await _container.DeleteItemAsync<T>(itemId, new PartitionKey(partitionKey));
        }
    }
}