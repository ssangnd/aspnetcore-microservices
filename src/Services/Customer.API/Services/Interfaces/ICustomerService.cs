namespace Product.API.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<IResult> GetCustomerByUsernameAsync(string username);
        Task<IResult> GetCustomerAsync();

    }
}
