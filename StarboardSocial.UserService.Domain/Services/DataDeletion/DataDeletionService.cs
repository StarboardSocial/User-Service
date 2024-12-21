using FluentResults;
using StarboardSocial.UserService.Domain.DataInterfaces;


namespace StarboardSocial.UserService.Domain.Services;

public interface IDataDeletionService
{
    Task<Result> RequestDataDeletion(string userId);
    Task<Result> DeleteUserData(string userId);
}

public class DataDeletionService(IDataDeletionRepository dataDeletionRepository) : IDataDeletionService
{
    private readonly IDataDeletionRepository _dataDeletionRepository = dataDeletionRepository;

    public async Task<Result> RequestDataDeletion(string userId) => await _dataDeletionRepository.RequestDataDeletion(userId);
    
    public async Task<Result> DeleteUserData(string userId) => await _dataDeletionRepository.DeleteUserData(userId);
    
}