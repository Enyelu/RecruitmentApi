using Microsoft.Azure.Cosmos;
using RecruitmentInfrastructure.Data.Interface;

namespace RecruitmentInfrastructure.Data
{
    public class CosmosDbService : ICosmoDbService
    {
        private readonly CosmosClient _cosmosClient;
        private readonly string _databaseId;
        private readonly string _containerId;
        private readonly Container _container;

        public CosmosDbService(string connectionString, string databaseId, string containerId)
        {
            _cosmosClient = new CosmosClient(connectionString);
            _databaseId = databaseId;
            _containerId = containerId;
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