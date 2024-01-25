using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.IoC
{
    public record UserInfo(Guid Id, Guid CustomerId, string FullName, IEnumerable<Claim> Claims);
    public static class ServiceTool
    {
        public static IServiceProvider ServiceProvider { get; private set; }
        public static IServiceCollection Create(IServiceCollection services)
        {
            ServiceProvider = services.BuildServiceProvider();
            return services;
        }
        public static T GetService<T>()
        {
            return ServiceProvider.GetService<T>();
        }
        public static IHttpContextAccessor GetHttpContextAccessor()
        {
            return GetService<IHttpContextAccessor>();
        }
        public static UserInfo? GetUserInfo()
        {
            var context = GetHttpContextAccessor();
            var customerId = context.HttpContext.User.FindFirst("customerId")?.Value;
            if (customerId == null) return null;
            var userId = context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return null;
            var fullName = context.HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
            var email = context.HttpContext.User.FindFirst(JwtRegisteredClaimNames.Email)?.Value;
            var roles = context.HttpContext.User.FindAll(ClaimTypes.Role);
            return new UserInfo(Guid.Parse(userId), Guid.Parse(customerId), fullName??"Unknown", roles);

        }



    }
}
