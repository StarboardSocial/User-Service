using Microsoft.Extensions.Hosting;

namespace StarboardSocial.UserService.Domain.Services;

public class DataDeletionListener(DataDeletionHandler dataDeletionHandler, DataDeletionConsumer dataDeletionConsumer)
    : BackgroundService
{
    private readonly DataDeletionConsumer _dataDeletionConsumer = dataDeletionConsumer;
    private readonly DataDeletionHandler _dataDeletionHandler = dataDeletionHandler;

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _dataDeletionConsumer.StartListening(async productEvent =>
        {
            await _dataDeletionHandler.Handle(productEvent);
        });

        return Task.CompletedTask;
    }
}