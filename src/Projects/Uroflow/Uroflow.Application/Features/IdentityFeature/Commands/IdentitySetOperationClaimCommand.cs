using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using MediatR;
using System.Text.Json.Serialization;
using Uroflow.Application.Features.IdentityFeature.Rules;
using Uroflow.Application.Features.IdentityFeature.Constants;
using Uroflow.Application.Services.Repositories;

namespace Uroflow.Application.Features.IdentityFeature.Commands;

public class IdentitySetOperationClaimCommand : IRequest<bool>, ISecuredRequest
{
    public Guid IdentityId { get; set; }
    public Guid OperationClaimId { get; set; }
    [JsonIgnore]
    public string[] Roles => new string[] { IdentityClaimConstants.UpdateOperationClaim };



    public class IdentitySetOperationClaimCommandHandler : IRequestHandler<IdentitySetOperationClaimCommand, bool>
    {
        private readonly IdentityBusinessRules _identityBusinessRules;
        private readonly IIdentityOperationClaimRepository _identityOperationClaimRepository;
        private readonly IIdentityRepository _identityRepository;

        public IdentitySetOperationClaimCommandHandler(
            IIdentityOperationClaimRepository identityOperationClaimRepository,
            IIdentityRepository identityRepository,
            IdentityBusinessRules identityBusinessRules)
        {
            _identityOperationClaimRepository = identityOperationClaimRepository;
            _identityRepository = identityRepository;
            _identityBusinessRules = identityBusinessRules;
        }

        public async Task<bool> Handle(IdentitySetOperationClaimCommand request, CancellationToken cancellationToken)
        {
            var identity = await _identityRepository.GetAsync(u => u.Id == request.IdentityId);
            await _identityBusinessRules.IdentityShouldExistWhenRequested(identity);
            var identityOperationClaim = await _identityOperationClaimRepository.GetAsync(l => l.IdentityId == request.IdentityId && l.OperationClaimId == request.OperationClaimId);
            if (identityOperationClaim is null)
                _identityOperationClaimRepository.Add(new()
                {
                    IdentityId = identity.Id,
                    OperationClaimId = request.OperationClaimId
                });
            return true;
        }
    }
}
