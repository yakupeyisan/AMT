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

public class ListDeletedIdentityQuery : IRequest<IdentityListModel>, ISecuredRequest
{
    public required PageRequest PageRequest { get; set; }

    public string[] Roles => new string[] { IdentityClaimConstants.ListDeleted };

    public class ListDeletedIdentityQueryHandler : IRequestHandler<ListDeletedIdentityQuery, IdentityListModel>
    {
        private readonly IIdentityRepository _identityRepository;
        private readonly IMapper _mapper;

        public ListDeletedIdentityQueryHandler(IIdentityRepository identityRepository, IMapper mapper)
        {
            _identityRepository = identityRepository;
            _mapper = mapper;
        }

        public async Task<IdentityListModel> Handle(ListDeletedIdentityQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Identity> identities = await _identityRepository.GetListDeletedAsync(i => i.IsDeleted == true, index: request.PageRequest.Page, size: request.PageRequest.PageSize);
            IdentityListModel mappedList = _mapper.Map<IdentityListModel>(identities);

            return mappedList;
        }
    }
}
