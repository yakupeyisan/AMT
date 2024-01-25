using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Security.Entities;
using MediatR;
using System.Text.Json.Serialization;
using Uroflow.Application.Features.IdentityFeature.Dtos;
using Uroflow.Application.Features.IdentityFeature.Rules;
using Uroflow.Application.Features.IdentityFeature.Constants;
using Uroflow.Application.Services;
using Uroflow.Application.Services.Repositories;
using Uroflow.Domain;

namespace Uroflow.Application.Features.IdentityFeature.Commands;
public class CreateIdentityCommand : IRequest<CreatedIdentityDto>, ISecuredRequest
{
    public string UserName { get; set; }
    public bool Status { get; set; }
    [JsonIgnore]
    public string[] Roles => new string[] { IdentityClaimConstants.Create };
    public class CreateIdentityCommandHandler : IRequestHandler<CreateIdentityCommand, CreatedIdentityDto>
    {
        private readonly IIdentityRepository _identityRepository;
        private readonly IMapper _mapper;
        private readonly IdentityBusinessRules _identityBusinessRules;

        public CreateIdentityCommandHandler(IIdentityRepository identityRepository, IMapper mapper, IdentityBusinessRules identityBusinessRules)
        {
            _identityRepository = identityRepository;
            _mapper = mapper;
            _identityBusinessRules = identityBusinessRules;
        }

        public async Task<CreatedIdentityDto> Handle(CreateIdentityCommand request, CancellationToken cancellationToken)
        {
            await _identityBusinessRules.UserNameMustBeUniqueWhenCreate(request.UserName);
            var mappedIdentity = _mapper.Map<Identity>(request);
            Identity createdIdentity = await _identityRepository.AddAsync(mappedIdentity);
            return _mapper.Map<CreatedIdentityDto>(createdIdentity);
        }
    }
}
