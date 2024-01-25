using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using MediatR;
using System.Text.Json.Serialization;
using Uroflow.Application.Features.IdentityFeature.Rules;
using Uroflow.Application.Features.IdentityFeature.Constants;
using Uroflow.Application.Services.Repositories;

namespace Uroflow.Application.Features.IdentityFeature.Commands;

public class IdentitySetAuthorityCommand : IRequest<bool>, ISecuredRequest
{
    public Guid IdentityId { get; set; } 
    public Guid AuthorityId { get; set; }
    [JsonIgnore]
    public string[] Roles => new string[] { IdentityClaimConstants.UpdateAuthority };

    public class IdentitySetAuthorityCommandHandler : IRequestHandler<IdentitySetAuthorityCommand, bool>
    {
        private readonly IdentityBusinessRules _identityBusinessRules;
        private readonly IIdentityAuthorityRepository _identityAuthorityRepository;
        private readonly IIdentityRepository _identityRepository;

        public IdentitySetAuthorityCommandHandler(
            IIdentityAuthorityRepository identityAuthorityRepository,
            IIdentityRepository identityRepository,
            IdentityBusinessRules identityBusinessRules)
        {
            _identityAuthorityRepository = identityAuthorityRepository;
            _identityRepository = identityRepository;
            _identityBusinessRules = identityBusinessRules;
        }

        public async Task<bool> Handle(IdentitySetAuthorityCommand request, CancellationToken cancellationToken)
        {
            var identity = await _identityRepository.GetAsync(u => u.Id == request.IdentityId);
            await _identityBusinessRules.IdentityShouldExistWhenRequested(identity);
            var identityAuthority = await _identityAuthorityRepository.GetAsync(l => l.IdentityId == request.IdentityId && l.AuthorityId == request.AuthorityId);
            if (identityAuthority is null)
                _identityAuthorityRepository.Add(new()
                {
                    IdentityId = identity.Id,
                    AuthorityId = request.AuthorityId
                });
            return true;
        }
    }
}
