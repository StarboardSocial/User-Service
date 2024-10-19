using FluentResults;
using Microsoft.EntityFrameworkCore;
using StarboardSocial.UserService.Data.Database;
using StarboardSocial.UserService.Data.Mappers;
using StarboardSocial.UserService.Data.Models;
using StarboardSocial.UserService.Domain.DataInterfaces;
using StarboardSocial.UserService.Domain.Models;

namespace StarboardSocial.UserService.Data.Repositories;

public class RegistrationRepository(StarboardDbContext dbContext) : IRegistrationRepository
{
    private readonly StarboardDbContext _dbContext = dbContext;


    public async Task<Result> Register(Profile profile)
    {
        ProfileDto profileDto = ProfileMapper.ProfileToDto(profile);
        bool alreadyExists = await _dbContext.Profiles.Where(foundProfile => foundProfile.Id == profileDto.Id).AnyAsync();
        if (alreadyExists)
        {
            return Result.Fail("An user with this ID is already registered");
        }
        await _dbContext.Profiles.AddAsync(profileDto);
        await _dbContext.SaveChangesAsync();
        return Result.Ok();
    }
}