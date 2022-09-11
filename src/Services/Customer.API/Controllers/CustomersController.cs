using Customer.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Product.API.Services.Interfaces;

namespace Customer.API.Controllers
{
    public static class CustomersController
    {
        public static void MapCustomersAPI(this WebApplication app)
        {
            app.MapGet("/api/customers", async ( ICustomerService customerService)
                => await customerService.GetCustomerAsync());
            //app.MapGet("/api/customers/{username}", async(string username, ICustomerService customerService)
            //    => await customerService.GetCustomerByUsernameAsync(username));

            app.MapGet("/api/customers/{username}", async (string username, ICustomerService customerService) =>
            {
                //var customer = await customerService.GetCustomerByUsernameAsync(username);
                //return customer != null ? Results.Ok(customer) : Results.NotFound();
                var result = await customerService.GetCustomerByUsernameAsync(username);
                return result != null ? result : Results.NotFound();
            });

            app.MapPost("/api/customers", async (Customer.API.Entities.Customer customer, ICustomerRepository customerRepository) =>
            {
                customerRepository.CreateAsync(customer);
                customerRepository.SaveChangesAsync();
            });

            app.MapDelete("/api/customers/{id}", async (int id, ICustomerRepository customerRepository) =>
            {
                var customer = await customerRepository.FindByCondition(x => x.Id.Equals(id)).SingleOrDefaultAsync();
                if (customer == null) return Results.NotFound();
                await customerRepository.DeleteAsync(customer);
                await customerRepository.SaveChangesAsync();
                return Results.NoContent();
            });

            //app.MapPut("/api/customers/{id}", async () =>
            //{

            //});
        }
    }
}
