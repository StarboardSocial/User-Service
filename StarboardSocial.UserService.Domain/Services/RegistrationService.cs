using FluentResults;
using StarboardSocial.UserService.Domain.DataInterfaces;
using StarboardSocial.UserService.Domain.Models;

namespace StarboardSocial.UserService.Domain.Services;

public interface IRegistrationService
{
    Task<Result> Register(Profile profile);
}

public class RegistrationService(IRegistrationRepository registrationRepository) : IRegistrationService
{
    private readonly IRegistrationRepository _registrationRepository = registrationRepository;
    public async Task<Result> Register(Profile profile) => await _registrationRepository.Register(profile);
    
}