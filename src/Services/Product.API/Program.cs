using Common.Logging;
using Product.API.Extentions;
using Serilog;

var builder = WebApplication.CreateBuilder(args); 


Log.Information("Starting Product API up");

try
{
    builder.Host.UseSerilog(Serilogger.Configure);
    builder.Host.AddAppConfigurations();

    // Add services to the container.
    builder.Services.AddInfrastructure();
   

    var app = builder.Build();
    app.UseInfrastructure();


    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Unhandled  exception");
}
finally
{
    Log.Information("Shut down Product API complate");
    Log.CloseAndFlush();
}


