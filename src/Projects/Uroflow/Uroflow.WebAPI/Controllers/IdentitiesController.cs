using Core.Application.Requests;
using Core.Persistence.Controllers;
using Microsoft.AspNetCore.Mvc;
using Uroflow.Application.Features.IdentityFeature.Commands;
using Uroflow.Application.Features.IdentityFeature.Dtos;
using Uroflow.Application.Features.IdentityFeature.Models;
using Uroflow.Application.Features.IdentityFeature.Queries;

namespace Uroflow.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class IdentitiesController : BaseController
{
    [HttpGet("GetList")]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        ListIdentityQuery getListIdentityQuery = new() { PageRequest = pageRequest };
        IdentityListModel result = await Mediator.Send(getListIdentityQuery);
        return Ok(result);
    }
    [HttpGet("GetListNotDeleted")]
    public async Task<IActionResult> GetListNotDeleted([FromQuery] PageRequest pageRequest)
    {
        ListNotDeletedIdentityQuery getListNotDeletedIdentityQuery = new() { PageRequest = pageRequest };
        IdentityListModel result = await Mediator.Send(getListNotDeletedIdentityQuery);
        return Ok(result);
    }
    [HttpGet("GetListDeleted")]
    public async Task<IActionResult> GetListDeleted([FromQuery] PageRequest pageRequest)
    {
        ListDeletedIdentityQuery getListDeletedIdentityQuery = new() { PageRequest = pageRequest };
        IdentityListModel result = await Mediator.Send(getListDeletedIdentityQuery);
        return Ok(result);
    }

    [HttpGet("GetById/{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        GetByIdIdentityQuery getIdentityQuery = new() { Id = id };
        IdentityGetByIdDto getIdentityResult = await Mediator.Send(getIdentityQuery);
        return Ok(getIdentityResult);
    }
    [HttpPost("Add")]
    public async Task<IActionResult> Add([FromBody] CreateIdentityCommand createdIdentity)
    {
        CreatedIdentityDto result = await Mediator.Send(createdIdentity);
        return Created("", result);
    }
    [HttpPost("Update")]
    public async Task<IActionResult> Update([FromBody] UpdateIdentityCommand updateIdentity)
    {
        UpdatedIdentityDto result = await Mediator.Send(updateIdentity);
        return Created("", result);
    }
    [HttpGet("DeleteById/{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        HardDeleteByIdIdentityCommand deleteByIdIdentityCommand = new() { Id = id };
        await Mediator.Send(deleteByIdIdentityCommand);
        return NoContent();
    }
    [HttpGet("SoftDeleteById/{id}")]
    public async Task<IActionResult> SoftDelete(Guid id)
    {
        SoftDeleteByIdIdentityCommand softDeleteByIdIdentityCommand = new() { Id = id };
        await Mediator.Send(softDeleteByIdIdentityCommand);
        return NoContent();
    }
    [HttpGet("Restore/{id}")]
    public async Task<IActionResult> Restore(Guid id)
    {
        RestoreByIdIdentityCommand restoreByIdIdentityCommand = new() { Id = id };
        await Mediator.Send(restoreByIdIdentityCommand);
        return NoContent();
    }
}