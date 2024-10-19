using FluentResults;
using StarboardSocial.UserService.Domain.Models;

namespace StarboardSocial.UserService.Domain.DataInterfaces;

public interface IRegistrationRepository
{
    Task<Result> Register(Profile profile);
}