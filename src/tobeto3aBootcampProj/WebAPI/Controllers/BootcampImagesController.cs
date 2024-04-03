using Application.Features.BootcampImages.Commands.Create;
using Application.Features.BootcampImages.Commands.Delete;
using Application.Features.BootcampImages.Commands.Update;
using Application.Features.BootcampImages.Queries.GetById;
using Application.Features.BootcampImages.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;
using Application.Services.BootcampImages;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BootcampImagesController : BaseController
{
    private readonly IBootcampImageService _bootcampImageService;

    public BootcampImagesController(IBootcampImageService bootcampImageService)
    {
        _bootcampImageService = bootcampImageService;
    }


    [HttpPost]
    public async Task<IActionResult> Add(IFormFile file,[FromBody] CreateBootcampImageCommand createBootcampImageCommand)
    {
        //CreatedBootcampImageResponse response = await Mediator.Send(createBootcampImageCommand);
        //return Created(uri: "", response);
        var result = await _bootcampImageService.AddAsync(file, createBootcampImageCommand);
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateBootcampImageCommand updateBootcampImageCommand)
    {
        UpdatedBootcampImageResponse response = await Mediator.Send(updateBootcampImageCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedBootcampImageResponse response = await Mediator.Send(new DeleteBootcampImageCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdBootcampImageResponse response = await Mediator.Send(new GetByIdBootcampImageQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListBootcampImageQuery getListBootcampImageQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListBootcampImageListItemDto> response = await Mediator.Send(getListBootcampImageQuery);
        return Ok(response);
    }
}