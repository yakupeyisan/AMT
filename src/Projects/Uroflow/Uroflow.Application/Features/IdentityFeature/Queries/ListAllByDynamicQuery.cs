using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using Core.Security.Entities;
using MediatR;
using Uroflow.Application.Features.IdentityFeature.Models;
using Uroflow.Application.Services.Repositories;

namespace Uroflow.Application.Features.IdentityFeature.Queries
{
    public class ListAllByDynamicQuery : IRequest<IdentityListModel>
    {
        public required PageRequest PageRequest { get; set; }
        public required List<Dynamic> Queries { get; set; }
        public class ListAllByDynamicQueryHandler : IRequestHandler<ListAllByDynamicQuery, IdentityListModel>
        {
            private readonly IIdentityRepository _identityRepository;
            private readonly IMapper _mapper;

            public ListAllByDynamicQueryHandler(IIdentityRepository identityRepository, IMapper mapper)
            {
                _identityRepository = identityRepository;
                _mapper = mapper;
            }

            public async Task<IdentityListModel> Handle(ListAllByDynamicQuery request, CancellationToken cancellationToken)
            {
                var query = _identityRepository.Query();
                if(request.Queries is not null && request.Queries.Any())
                    foreach (var item in request.Queries)
                    {
                        query = query.ToDynamic(item);
                    }
                var identities = await query.ToPaginateAsync(request.PageRequest.Page,request.PageRequest.PageSize);
                IdentityListModel mappedList = _mapper.Map<IdentityListModel>(identities);
                return mappedList;
            }
        }
    }
}
