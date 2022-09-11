using Customer.API.Repositories.Interfaces;
using Product.API.Services.Interfaces;

namespace Product.API.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;
        public CustomerService(ICustomerRepository repository)
        {
            _repository = repository;
        }
        public async Task<IResult> GetCustomerByUsernameAsync(string username) 
            => Results.Ok(await _repository.GetCustomerUserNameAsync(username));

        public async Task<IResult> GetCustomerAsync() => Results.Ok(await _repository.GetCustomers());
        
    }
}
