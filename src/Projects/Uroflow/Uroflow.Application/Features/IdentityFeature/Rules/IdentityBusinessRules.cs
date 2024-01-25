using Core.CrossCuttingConcerns.Exceptions;
using Core.CrossCuttingConcers.Constants;
using Core.Security.Entities;
using Uroflow.Application.Services.Repositories;
using Uroflow.Domain.Entities;
namespace Uroflow.Application.Features.IdentityFeature.Rules
{
    public class IdentityBusinessRules
    {
        private readonly IIdentityRepository _identityRepository;

        public IdentityBusinessRules(IIdentityRepository identityRepository)
        {
            _identityRepository = identityRepository;
        }
        public async Task IdentityShouldExistWhenRequested(Identity? identity)
        {
            if (identity == null) throw new BusinessException("Requested record does not exist", ErrorConstants.RequestedRecordDoesNotExist);
            await Task.CompletedTask;
        }
        public async Task UserNameMustBeUniqueWhenCreate(string userName)
        {
            var identity = await _identityRepository.GetAsync(i => i.UserName == userName);
            if (identity != null) throw new BusinessException("Username already taken", ErrorConstants.UserNameAlreadyTaken);
            await Task.CompletedTask;
        }
        public async Task UserNameMustBeUniqueWhenUpdate(Guid id, string userName)
        {
            var identity = await _identityRepository.GetAsync(i => i.UserName == userName && i.Id != id);
            if (identity != null) throw new BusinessException("Username already taken", ErrorConstants.UserNameAlreadyTaken);
            await Task.CompletedTask;
        }
    }
}
