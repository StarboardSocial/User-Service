using FluentResults;
using Microsoft.EntityFrameworkCore;
using StarboardSocial.UserService.Data.Database;
using StarboardSocial.UserService.Data.Mappers;
using StarboardSocial.UserService.Data.Models;
using StarboardSocial.UserService.Domain.DataInterfaces;
using StarboardSocial.UserService.Domain.Models;

namespace StarboardSocial.UserService.Data.Repositories;

public class ProfileRepository(StarboardDbContext dbContext) : IProfileRepository
{
    private readonly StarboardDbContext _dbContext = dbContext;
    
    public async Task<Result<Profile>> GetProfile(string userId)
    {
        ProfileDto? profileDto = await _dbContext.Profiles
            .Where(profile => profile.Id == userId)
            .FirstOrDefaultAsync();

        if (profileDto == null) return Result.Fail("Profile with given ID doesn't exist");
        {
            Profile profile = ProfileMapper.DtoToProfile(profileDto);
            return Result.Ok(profile);
        }

    }
}