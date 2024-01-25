using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Core.Persistence.Repositories;
using Core.Security.Entities;
using MediatR;
using Newtonsoft.Json;
using Uroflow.Application.Features.IdentityFeature.Constants;
using Uroflow.Application.Features.IdentityFeature.Models;
using Uroflow.Application.Services.Repositories;

namespace Uroflow.Application.Features.IdentityFeature.Queries;

public class ListIdentityQuery : IRequest<IdentityListModel>, ISecuredRequest
{
    public PageRequest PageRequest { get; set; }


    public string[] Roles => new string[] { IdentityClaimConstants.List };

    public class IdentityListQueryHandler : IRequestHandler<ListIdentityQuery, IdentityListModel>
    {
        private readonly IIdentityRepository _identityRepository;
        private readonly IMapper _mapper;

        public IdentityListQueryHandler(IIdentityRepository identityRepository, IMapper mapper)
        {
            _identityRepository = identityRepository;
            _mapper = mapper;
        }

        public async Task<IdentityListModel> Handle(ListIdentityQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Identity> identities = await _identityRepository.GetListAsync(index: request.PageRequest.Page, size: request.PageRequest.PageSize);
            IdentityListModel mappedList = _mapper.Map<IdentityListModel>(identities);

            return mappedList;
        }
    }
}
