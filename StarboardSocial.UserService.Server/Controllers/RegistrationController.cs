using System.Security.Claims;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using StarboardSocial.UserService.Domain.Models;
using StarboardSocial.UserService.Domain.Services;
using StarboardSocial.UserService.Server.Helpers;
using StarboardSocial.UserService.Server.Mappers;
using StarboardSocial.UserService.Server.Models;

namespace StarboardSocial.UserService.Server.Controllers;

[ApiController]
[Route("registration")]
public class RegistrationController(IRegistrationService registrationService): ControllerBase
{
    private readonly IRegistrationService _registrationService = registrationService;
    
    [HttpPost]
    public async Task<IActionResult> Register([FromBody]ProfileCreationModel profileCreationModel)
    {
        try
        {
            Profile profile = ProfileMapper.ProfileCreationModelToProfile(profileCreationModel, UserIdHelper.GetUserId(Request));
            Result result = await _registrationService.Register(profile);
            return result.IsSuccess ? Ok(result.Successes) : BadRequest(result.Errors);
        }
        catch (UnauthorizedAccessException e)
        {
            return Unauthorized(e.Message);
        }
    }
}