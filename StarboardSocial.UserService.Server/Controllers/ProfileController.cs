using FluentResults;
using Microsoft.AspNetCore.Mvc;
using StarboardSocial.UserService.Domain.Models;
using StarboardSocial.UserService.Domain.Services;
using StarboardSocial.UserService.Server.Helpers;
using StarboardSocial.UserService.Server.Mappers;

namespace StarboardSocial.UserService.Server.Controllers;

[ApiController]
[Route("profile")]
public class ProfileController(IProfileService profileService) : ControllerBase
{
    private readonly IProfileService _profileService = profileService;
    
    [HttpGet]
    public async Task<IActionResult> GetProfile()
    {
        try
        {
            Result<Profile> result = await _profileService.GetProfile(UserIdHelper.GetUserId(Request));
            ProfileReturnModel profileReturnModel = ProfileMapper.ProfileToProfileReturnModel(result.Value);
            return result.IsSuccess ? Ok(profileReturnModel) : BadRequest(result.Errors);
        }
        catch (UnauthorizedAccessException e)
        {
            return Unauthorized(e.Message);
        }
    }
}