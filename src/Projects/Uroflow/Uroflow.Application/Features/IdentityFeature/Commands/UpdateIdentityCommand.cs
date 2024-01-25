using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Security.Entities;
using MediatR;
using System.Text.Json.Serialization;
using Uroflow.Application.Features.IdentityFeature.Dtos;
using Uroflow.Application.Features.IdentityFeature.Rules;
using Uroflow.Application.Features.IdentityFeature.Constants;
using Uroflow.Application.Services.Repositories;

namespace Uroflow.Application.Features.IdentityFeature.Commands;

public class UpdateIdentityCommand : IRequest<UpdatedIdentityDto>, ISecuredRequest
{
    public Guid Id { get; set; }
    public required string UserName { get; set; }
    [JsonIgnore]
    public string[] Roles => new string[] { IdentityClaimConstants.UpdateUserName };
    public class UpdateIdentityCommandHandler : IRequestHandler<UpdateIdentityCommand, UpdatedIdentityDto>
    {
        private readonly IIdentityRepository _identityRepository;
        private readonly IMapper _mapper;
        private readonly IdentityBusinessRules _identityBusinessRules;

        public UpdateIdentityCommandHandler(IIdentityRepository identityRepository, IMapper mapper, IdentityBusinessRules identityBusinessRules)
        {
            _identityRepository = identityRepository;
            _mapper = mapper;
            _identityBusinessRules = identityBusinessRules;
        }

        public async Task<UpdatedIdentityDto> Handle(UpdateIdentityCommand request, CancellationToken cancellationToken)
        {
            await _identityBusinessRules.UserNameMustBeUniqueWhenUpdate(request.Id, request.UserName);
            Identity mappedIdentity = _mapper.Map<Identity>(request);
            Identity updatedIdentity = await _identityRepository.UpdateAsync(mappedIdentity);
            return _mapper.Map<UpdatedIdentityDto>(updatedIdentity);
        }
    }
}
