using BuyMyHouse.DataAccess;
using BuyMyHouse.Repositories;
using BuyMyHouse.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Blob = BuyMyHouse.Repositories.Blob;

var host = new HostBuilder()
    .ConfigureAppConfiguration(configs =>
    {
        configs.AddJsonFile("local.settings.json", optional: true, reloadOnChange: true);
    })
    .ConfigureServices(services =>
    {
        services.AddTransient<IBuyMyHouseDbContext, BuyMyHouseDbContext>();
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<IHouseService, HouseService>();
        services.AddTransient<IHouseRepository, HouseRepository>();
        //services.AddTransient<IMortgageService, MortgageService>();
        services.AddSingleton<IBlob, Blob>();
        services.AddSingleton(sp => sp.GetRequiredService<ILoggerFactory>().CreateLogger("DefaultLogger"));
    })
                .ConfigureFunctionsWorkerDefaults()
                .Build();

host.Run();
