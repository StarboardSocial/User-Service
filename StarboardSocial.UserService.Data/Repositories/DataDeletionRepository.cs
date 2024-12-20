using System.Text;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;
using StarboardSocial.UserService.Data.Database;
using StarboardSocial.UserService.Domain.DataInterfaces;

namespace StarboardSocial.UserService.Data.Repositories;

public class DataDeletionRepository : IDataDeletionRepository
{
    private readonly IChannel _channel;
    private readonly StarboardDbContext _dbContext;

    public DataDeletionRepository(IChannel channel, StarboardDbContext dbContext)
    {
        _channel = channel;
        _dbContext = dbContext;
        
        _channel.ExchangeDeclareAsync(
            "data-deletion",
            ExchangeType.Topic,
            true,
            false
        );
    }

    public async Task<Result> RequestDataDeletion(string userId)
    {
        byte[] body = Encoding.UTF8.GetBytes(userId);

        await _channel.BasicPublishAsync(
            exchange: "data-deletion",
            routingKey: "data-deletion.request",
            body: body
            );
        Console.WriteLine($" [x] Sent {userId}");

        return Result.Ok();
    }

    public async Task<Result> DeleteUserData(string userId)
    {
        bool success = await _dbContext.Profiles
            .Where(profile => profile.Id == userId)
            .ExecuteDeleteAsync() == 1;
        
        await _dbContext.SaveChangesAsync();
        
        return success ? Result.Ok() : Result.Fail("User not found");
    }
}