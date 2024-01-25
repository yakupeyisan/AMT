using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using MediatR;
using System.Text.Json.Serialization;
using Uroflow.Application.Features.IdentityFeature.Dtos;
using Uroflow.Application.Features.IdentityFeature.Rules;
using Uroflow.Application.Features.IdentityFeature.Constants;
using Uroflow.Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.CrossCuttingConcers.Constants;
using Uroflow.Domain.Entities;

namespace Uroflow.Application.Features.IdentityFeature.Commands;

public class UpdateIdentityStatusCommand : IRequest<bool>, ISecuredRequest
{
    public Guid Id { get; set; }
    public bool Status { get; set; }
    [JsonIgnore]
    public string[] Roles => new string[] { IdentityClaimConstants.UpdateStatus };
    public class UpdateIdentityStatusCommandHandler : IRequestHandler<UpdateIdentityStatusCommand, bool>
    {
        private readonly IIdentityRepository _identityRepository;
        private readonly IMapper _mapper;
        private readonly IdentityBusinessRules _identityBusinessRules;

        public UpdateIdentityStatusCommandHandler(IIdentityRepository identityRepository, IMapper mapper, IdentityBusinessRules identityBusinessRules)
        {
            _identityRepository = identityRepository;
            _mapper = mapper;
            _identityBusinessRules = identityBusinessRules;
        }

        public async Task<bool> Handle(UpdateIdentityStatusCommand request, CancellationToken cancellationToken)
        {
            var findedIdentity = await _identityRepository.GetAsync(identity => identity.Id == request.Id);
            await _identityBusinessRules.IdentityShouldExistWhenRequested(findedIdentity);
            findedIdentity.Status = request.Status;
            await _identityRepository.UpdateAsync(findedIdentity);
            return true;
        }
    }
}
