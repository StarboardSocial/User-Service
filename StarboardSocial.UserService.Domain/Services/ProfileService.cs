using FluentResults;
using StarboardSocial.UserService.Domain.DataInterfaces;
using StarboardSocial.UserService.Domain.Models;

namespace StarboardSocial.UserService.Domain.Services;

public interface IProfileService
{
    Task<Result<Profile>> GetProfile(string userId);
}

public class ProfileService(IProfileRepository profileRepository) : IProfileService
{
    private readonly IProfileRepository _profileRepository = profileRepository;
    public async Task<Result<Profile>> GetProfile(string userId) => await _profileRepository.GetProfile(userId);
}