using Core.Persistence.Paging;
using Uroflow.Application.Features.IdentityFeature.Dtos;

namespace Uroflow.Application.Features.IdentityFeature.Models;
public class IdentityListModel : BasePageableModel
{
    public IList<IdentityListDto> Items { get; set; }

}
