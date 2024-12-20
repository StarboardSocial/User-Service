using FluentResults;
using Microsoft.AspNetCore.Mvc;
using StarboardSocial.UserService.Domain.Services;
using StarboardSocial.UserService.Server.Helpers;

namespace StarboardSocial.UserService.Server.Controllers;

[ApiController]
[Route("data-deletion")]
public class DataDeletionController(IDataDeletionService dataDeletionService) : ControllerBase
{
    private readonly IDataDeletionService _dataDeletionService = dataDeletionService;

    [HttpPost]
    public async Task<IActionResult> RequestDataDeletion()
    {
        try
        {
            Result result = await _dataDeletionService.RequestDataDeletion(UserIdHelper.GetUserId(Request));
            return result.IsSuccess ? Ok() : BadRequest(result.Errors);
        }
        catch (UnauthorizedAccessException e)
        {
            return Unauthorized(e.Message);
        }
    }
}