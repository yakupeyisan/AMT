using AutoMapper;
using Core.Security.Entities;
using MediatR;
using Uroflow.Application.Features.IdentityFeature.Dtos;
using Uroflow.Application.Features.IdentityFeature.Rules;
using Uroflow.Application.Services.Repositories;
namespace Uroflow.Application.Features.IdentityFeature.Queries
{
    public class GetByIdIdentityQuery : IRequest<IdentityGetByIdDto>
    {
        public Guid Id { get; set; }

        public class GetByIdIdentityQueryHandler : IRequestHandler<GetByIdIdentityQuery, IdentityGetByIdDto>
        {
            private readonly IIdentityRepository _identityRepository;
            private readonly IMapper _mapper;
            private readonly IdentityBusinessRules _identityBusinessRules;

            public GetByIdIdentityQueryHandler(IIdentityRepository identityRepository, IMapper mapper, IdentityBusinessRules identityBusinessRules)
            {
                _identityRepository = identityRepository;
                _mapper = mapper;
                _identityBusinessRules = identityBusinessRules;
            }

            public async Task<IdentityGetByIdDto> Handle(GetByIdIdentityQuery request, CancellationToken cancellationToken)
            {
                Identity? identity = await _identityRepository.GetAsync(i => i.Id == request.Id);

                await _identityBusinessRules.IdentityShouldExistWhenRequested(identity);

                return _mapper.Map<IdentityGetByIdDto>(identity);
            }
        }
    }
}
