using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uroflow.Application.Features.IdentityFeature.Dtos;
public class IdentityGetByIdDto
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public bool Status { get; set; }

}