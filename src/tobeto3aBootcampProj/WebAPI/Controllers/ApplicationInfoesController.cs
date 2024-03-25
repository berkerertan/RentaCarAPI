using Application.Features.ApplicationInfoes.Commands.Create;
using Application.Features.ApplicationInfoes.Commands.Delete;
using Application.Features.ApplicationInfoes.Commands.Update;
using Application.Features.ApplicationInfoes.Queries.GetById;
using Application.Features.ApplicationInfoes.Queries.GetList;
using Microsoft.AspNetCore.Mvc;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ApplicationInfoesController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateApplicationInfoCommand createApplicationInfoCommand)
    {
        CreatedApplicationInfoResponse response = await Mediator.Send(createApplicationInfoCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateApplicationInfoCommand updateApplicationInfoCommand)
    {
        UpdatedApplicationInfoResponse response = await Mediator.Send(updateApplicationInfoCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedApplicationInfoResponse response = await Mediator.Send(new DeleteApplicationInfoCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdApplicationInfoResponse response = await Mediator.Send(new GetByIdApplicationInfoQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListApplicationInfoQuery getListApplicationInfoQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListApplicationInfoListItemDto> response = await Mediator.Send(getListApplicationInfoQuery);
        return Ok(response);
    }
}
