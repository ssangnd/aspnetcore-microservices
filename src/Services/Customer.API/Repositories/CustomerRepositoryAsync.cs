using Contracts.Common.Interfaces;
using Customer.API.Persistence;
using Customer.API.Repositories.Interfaces;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace Customer.API.Repositories
{
    //public class CustomerRepositoryAsync : RepositoryBaseAsyncAsync<Entities.Customer, int, CustomerContext>, ICustomerRepository
    //{
    public class CustomerRepositoryAsync : RepositoryQueryBase<Entities.Customer, int, CustomerContext>, ICustomerRepository
    {
        //public CustomerRepositoryAsync(CustomerContext dbContext, IUnitOfWork<CustomerContext> unitOfWork):base(dbContext,unitOfWork)
        //{

        //}

        public CustomerRepositoryAsync(CustomerContext dbContext) : base(dbContext)
        {

        }

        public Task<Entities.Customer> GetCustomerUserNameAsync(string username)
            =>  FindByCondition(x => x.UserName.Equals(username)).SingleOrDefaultAsync();

        //public async Task<IEnumerable<Entities.Customer>> GetCustomers() => await FindAll().ToListAsync();

    }
}
