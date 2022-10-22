using Inventory.Product.API.Entities;
using MongoDB.Driver;

namespace Inventory.Product.API.Repositories.Abstraction
{
    public interface IMongoDbRepositoryBase<T> where T: MogoEntity
    {
        IMongoCollection<T> FindAlll(ReadPreference? readPreference=null);
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(string id);

    }
}
