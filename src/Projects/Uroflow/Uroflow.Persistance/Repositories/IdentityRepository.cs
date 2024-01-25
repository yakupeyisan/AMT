using Core.Persistence.Repositories;
using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uroflow.Application.Services.Repositories;
using Uroflow.Domain.Entities;
using Uroflow.Persistance.Contexts;

namespace Uroflow.Persistance.Repositories;

internal class IdentityRepository : EfBaseTimeStampRepositoryBase<Identity, UroflowDbContext>, IIdentityRepository

{
    public IdentityRepository(UroflowDbContext context) : base(context)
    {
    }
}
public class IdentityAuthorityRepository : EfRepositoryBase<IdentityAuthority, UroflowDbContext>, IIdentityAuthorityRepository
{
    public IdentityAuthorityRepository(UroflowDbContext context) : base(context) { }
}
public class IdentityOperationClaimRepository : EfRepositoryBase<IdentityOperationClaim, UroflowDbContext>, IIdentityOperationClaimRepository
{
    public IdentityOperationClaimRepository(UroflowDbContext context) : base(context) { }
}
public class AuthorityOperationClaimRepository : EfRepositoryBase<AuthorityOperationClaim, UroflowDbContext>, IAuthorityOperationClaimRepository
{
    public AuthorityOperationClaimRepository(UroflowDbContext context) : base(context) { }
}
public class UserRepository : EfRepositoryBase<User, UroflowDbContext>, IUserRepository
{
    public UserRepository(UroflowDbContext context) : base(context) { }
}
public class CustomerRepository : EfRepositoryBase<Customer, UroflowDbContext>, ICustomerRepository
{
    public CustomerRepository(UroflowDbContext context) : base(context) { }
}
public class CustomerContactRepository : EfRepositoryBase<CustomerContact, UroflowDbContext>, ICustomerContactRepository
{
    public CustomerContactRepository(UroflowDbContext context) : base(context) { }
}
public class CustomerAreaRepository : EfRepositoryBase<CustomerArea, UroflowDbContext>, ICustomerAreaRepository
{
    public CustomerAreaRepository(UroflowDbContext context) : base(context) { }
}
public class DeviceRepository : EfRepositoryBase<Device, UroflowDbContext>, IDeviceRepository
{
    public DeviceRepository(UroflowDbContext context) : base(context) { }
}
public class DeviceErrorRepository : EfRepositoryBase<DeviceError, UroflowDbContext>, IDeviceErrorRepository
{
    public DeviceErrorRepository(UroflowDbContext context) : base(context) { }
}
public class DeviceErrorMessageRepository : EfRepositoryBase<DeviceErrorMessage, UroflowDbContext>, IDeviceErrorMessageRepository
{
    public DeviceErrorMessageRepository(UroflowDbContext context) : base(context) { }
}
public class DeviceErrorStatusLogRepository : EfRepositoryBase<DeviceErrorStatusLog, UroflowDbContext>, IDeviceErrorStatusLogRepository
{
    public DeviceErrorStatusLogRepository(UroflowDbContext context) : base(context) { }
}
public class DeviceTypeRepository : EfRepositoryBase<DeviceType, UroflowDbContext>, IDeviceTypeRepository
{
    public DeviceTypeRepository(UroflowDbContext context) : base(context) { }
}
public class VerificationCodeRepository : EfRepositoryBase<VerificationCode, UroflowDbContext>, IVerificationCodeRepository
{
    public VerificationCodeRepository(UroflowDbContext context) : base(context) { }
}