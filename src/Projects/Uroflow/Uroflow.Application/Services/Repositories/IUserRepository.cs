using Core.Persistence.Repositories;
using Core.Security.Entities;
using Uroflow.Domain.Entities;

namespace Uroflow.Application.Services.Repositories;
public interface IIdentityRepository : IAsyncBaseTimeStampRepository<Identity>, IBaseTimeStampRepository<Identity>
{
}
public interface IIdentityAuthorityRepository : IAsyncRepository<IdentityAuthority>, IRepository<IdentityAuthority>
{
}
public interface IIdentityOperationClaimRepository : IAsyncRepository<IdentityOperationClaim>, IRepository<IdentityOperationClaim>
{
}
public interface IAuthorityOperationClaimRepository : IAsyncRepository<AuthorityOperationClaim>, IRepository<AuthorityOperationClaim>
{
}
public interface IUserRepository : IAsyncRepository<User>, IRepository<User>
{
}
public interface ICustomerRepository : IAsyncRepository<Customer>, IRepository<Customer>
{
}
public interface ICustomerContactRepository : IAsyncRepository<CustomerContact>, IRepository<CustomerContact>
{
}
public interface ICustomerAreaRepository : IAsyncRepository<CustomerArea>, IRepository<CustomerArea>
{
}
public interface IDeviceRepository : IAsyncRepository<Device>, IRepository<Device>
{
}
public interface IDeviceErrorRepository : IAsyncRepository<DeviceError>, IRepository<DeviceError>
{
}
public interface IDeviceErrorMessageRepository : IAsyncRepository<DeviceErrorMessage>, IRepository<DeviceErrorMessage>
{
}
public interface IDeviceErrorStatusLogRepository : IAsyncRepository<DeviceErrorStatusLog>, IRepository<DeviceErrorStatusLog>
{
}
public interface IDeviceTypeRepository : IAsyncRepository<DeviceType>, IRepository<DeviceType>
{
}
public interface IVerificationCodeRepository : IAsyncRepository<VerificationCode>, IRepository<VerificationCode>
{
}