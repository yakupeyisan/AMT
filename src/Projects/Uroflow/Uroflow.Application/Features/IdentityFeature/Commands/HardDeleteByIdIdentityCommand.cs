using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using MediatR;
using System.Text.Json.Serialization;
using Uroflow.Application.Features.IdentityFeature.Rules;
using Uroflow.Application.Features.IdentityFeature.Constants;
using Uroflow.Application.Services.Repositories;

namespace Uroflow.Application.Features.IdentityFeature.Commands;

public class HardDeleteByIdIdentityCommand : IRequest<bool>, ISecuredRequest
{
    public Guid Id { get; set; }
    [JsonIgnore]
    public string[] Roles => new string[] { IdentityClaimConstants.Delete };

    public class HardDeleteByIdIdentityCommandHandler : IRequestHandler<HardDeleteByIdIdentityCommand, bool>
    {
        private readonly IIdentityRepository _identityRepository;
        private readonly IdentityBusinessRules _identityBusinessRules;

        public HardDeleteByIdIdentityCommandHandler(IIdentityRepository identityRepository, IdentityBusinessRules identityBusinessRules)
        {
            _identityRepository = identityRepository;
            _identityBusinessRules = identityBusinessRules;
        }
        public async Task<bool> Handle(HardDeleteByIdIdentityCommand request, CancellationToken cancellationToken)
        {
            var identity = await _identityRepository.GetAsync(e => e.Id == request.Id);
            await _identityBusinessRules.IdentityShouldExistWhenRequested(identity);
            await _identityRepository.DeleteAsync(identity);
            return true;
        }
    }
}
