using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Uroflow.Application.Services.Repositories;
using Uroflow.Persistance.Contexts;
using Uroflow.Persistance.Repositories;

namespace Uroflow.Persistance;

public static class PersistenceServiceRegistration
{
    public static IConfiguration Configuration;
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        Configuration = configuration;
        services.AddDbContext<UroflowDbContext>(options =>
                                                 options.UseSqlServer(
                                                     configuration.GetConnectionString("Uroflow")
                                                 ));
        services.AddScoped<IIdentityRepository, IdentityRepository>(); 
        services.AddScoped<IIdentityAuthorityRepository, IdentityAuthorityRepository>();
        services.AddScoped<IIdentityOperationClaimRepository, IdentityOperationClaimRepository>();
        services.AddScoped<IAuthorityOperationClaimRepository, AuthorityOperationClaimRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<ICustomerContactRepository, CustomerContactRepository>();
        services.AddScoped<ICustomerAreaRepository, CustomerAreaRepository>();
        services.AddScoped<IDeviceRepository, DeviceRepository>();
        services.AddScoped<IDeviceErrorRepository, DeviceErrorRepository>();
        services.AddScoped<IDeviceErrorMessageRepository, DeviceErrorMessageRepository>();
        services.AddScoped<IDeviceErrorStatusLogRepository, DeviceErrorStatusLogRepository>();
        services.AddScoped<IDeviceTypeRepository, DeviceTypeRepository>();
        services.AddScoped<IVerificationCodeRepository, VerificationCodeRepository>();

        return services;
    }
}