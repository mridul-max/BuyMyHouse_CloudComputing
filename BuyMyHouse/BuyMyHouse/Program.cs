using BuyMyHouse.DataAccess;
using BuyMyHouse.Repositories;
using BuyMyHouse.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection.Metadata;

var host = new HostBuilder()
    .ConfigureServices(services =>
    {
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<IBuyMyHouseDbContext, BuyMyHouseDbContext>();
    })
                .ConfigureFunctionsWorkerDefaults()
                .Build();

host.Run();
