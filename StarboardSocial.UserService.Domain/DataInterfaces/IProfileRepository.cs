using FluentResults;
using StarboardSocial.UserService.Domain.Models;

namespace StarboardSocial.UserService.Domain.DataInterfaces;

public interface IProfileRepository
{
    Task<Result<Profile>> GetProfile(string userId);
}