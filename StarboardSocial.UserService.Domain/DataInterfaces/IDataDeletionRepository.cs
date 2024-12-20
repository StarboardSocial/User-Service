using FluentResults;

namespace StarboardSocial.UserService.Domain.DataInterfaces;

public interface IDataDeletionRepository
{
    Task<Result> RequestDataDeletion(string userId);
    Task<Result> DeleteUserData(string userId);
}