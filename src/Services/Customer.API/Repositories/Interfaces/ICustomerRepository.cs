using Contracts.Common.Interfaces;
using Customer.API.Persistence;

namespace Customer.API.Repositories.Interfaces
{
    //public interface ICustomerRepository : IRepositoryQueryBase<Entities.Customer, int, CustomerContext>
    public interface ICustomerRepository : IRepositoryBaseAsync<Entities.Customer, int, CustomerContext>
    {
        Task<Entities.Customer> GetCustomerUserNameAsync(string username);
        Task<IEnumerable<Entities.Customer>> GetCustomers();
    }
}
