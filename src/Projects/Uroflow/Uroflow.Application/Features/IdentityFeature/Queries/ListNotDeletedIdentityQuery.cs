using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Core.Security.Entities;
using MediatR;
using Uroflow.Application.Features.IdentityFeature.Constants;
using Uroflow.Application.Features.IdentityFeature.Models;
using Uroflow.Application.Services.Repositories;

namespace Uroflow.Application.Features.IdentityFeature.Queries;

public class ListNotDeletedIdentityQuery : IRequest<IdentityListModel>, ISecuredRequest
{
    public PageRequest PageRequest { get; set; }


    public string[] Roles => new string[] { IdentityClaimConstants.ListNotDeleted };

    public class ListNotDeletedIdentityQueryHandler : IRequestHandler<ListNotDeletedIdentityQuery, IdentityListModel>
    {
        private readonly IIdentityRepository _identityRepository;
        private readonly IMapper _mapper;

        public ListNotDeletedIdentityQueryHandler(IIdentityRepository identityRepository, IMapper mapper)
        {
            _identityRepository = identityRepository;
            _mapper = mapper;
        }

        public async Task<IdentityListModel> Handle(ListNotDeletedIdentityQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Identity> identities = await _identityRepository.GetListNotDeletedAsync( index: request.PageRequest.Page, size: request.PageRequest.PageSize);
            IdentityListModel mappedList = _mapper.Map<IdentityListModel>(identities);

            return mappedList;
        }
    }
}
