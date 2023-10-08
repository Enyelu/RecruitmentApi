namespace RecruitmentInfrastructure.Data.Interface
{
    public interface ICosmoDbService
    {
        Task InitializeAsync();
        Task SaveAsync<T>(T item);
        Task UpdateAsync<T>(T updatedItem);
        Task SaveManyAsync<T>(IEnumerable<T> items);
        Task<IEnumerable<T>> GetManyAsync<T>(string query);
        Task DeleteAsync<T>(string itemId, string partitionKey);
    }
}