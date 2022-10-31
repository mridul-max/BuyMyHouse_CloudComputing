using BuyMyHouse.DataAccess;
using BuyMyHouse.Repositories;
using BuyMyHouse.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Blob = BuyMyHouse.Repositories.Blob;

var host = new HostBuilder()
    .ConfigureServices(services =>
    {
        services.AddTransient<IBuyMyHouseDbContext, BuyMyHouseDbContext>();
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<IHouseService, HouseService>();
        services.AddTransient<IHouseRepository, HouseRepository>();
        services.AddTransient<IMortgageService, MortgageService>();
        services.AddSingleton<IBlob, Blob>();
    })
                .ConfigureFunctionsWorkerDefaults()
                .Build();

host.Run();
