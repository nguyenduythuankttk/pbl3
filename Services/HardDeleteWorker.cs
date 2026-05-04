using Backend.Data;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services;

public class HardDeleteWorker : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<HardDeleteWorker> _logger;
    private readonly TimeSpan _interval;
    private readonly TimeSpan _retentionPeriod;

    public HardDeleteWorker(IServiceScopeFactory scopeFactory, ILogger<HardDeleteWorker> logger, IConfiguration config)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
        _interval = TimeSpan.FromHours(config.GetValue<double>("HardDelete:IntervalHours", 24));
        _retentionPeriod = TimeSpan.FromDays(config.GetValue<double>("HardDelete:RetentionDays", 30));
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await RunAsync();
            await Task.Delay(_interval, stoppingToken);
        }
    }

    private async Task RunAsync()
    {
        using var scope = _scopeFactory.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var cutoff = DateTime.UtcNow - _retentionPeriod;

        try
        {
            var deleted = 0;

            deleted += await db.User.Where(x => x.DeletedAt != null && x.DeletedAt < cutoff).ExecuteDeleteAsync();
            deleted += await db.Store.Where(x => x.DeletedAt != null && x.DeletedAt < cutoff).ExecuteDeleteAsync();
            deleted += await db.Supplier.Where(x => x.DeletedAt != null && x.DeletedAt < cutoff).ExecuteDeleteAsync();
            deleted += await db.Category.Where(x => x.DeletedAt != null && x.DeletedAt < cutoff).ExecuteDeleteAsync();
            deleted += await db.Product.Where(x => x.DeletedAt != null && x.DeletedAt < cutoff).ExecuteDeleteAsync();
            deleted += await db.ProductVarient.Where(x => x.DeletedAt != null && x.DeletedAt < cutoff).ExecuteDeleteAsync();
            deleted += await db.Combo.Where(x => x.DeletedAt != null && x.DeletedAt < cutoff).ExecuteDeleteAsync();
            deleted += await db.Ingredient.Where(x => x.DeletedAt != null && x.DeletedAt < cutoff).ExecuteDeleteAsync();
            deleted += await db.Warehouse.Where(x => x.DeletedAt != null && x.DeletedAt < cutoff).ExecuteDeleteAsync();
            deleted += await db.DiningTable.Where(x => x.DeletedAt != null && x.DeletedAt < cutoff).ExecuteDeleteAsync();
            deleted += await db.Booking.Where(x => x.DeletedAt != null && x.DeletedAt < cutoff).ExecuteDeleteAsync();
            deleted += await db.Ticket.Where(x => x.DeletedAt != null && x.DeletedAt < cutoff).ExecuteDeleteAsync();
            deleted += await db.Receipt.Where(x => x.DeletedAt != null && x.DeletedAt < cutoff).ExecuteDeleteAsync();
            deleted += await db.DeliveryInfo.Where(x => x.DeletedAt != null && x.DeletedAt < cutoff).ExecuteDeleteAsync();
            deleted += await db.PurchaseOrder.Where(x => x.DeletedAt != null && x.DeletedAt < cutoff).ExecuteDeleteAsync();
            deleted += await db.Shift.Where(x => x.DeletedAt != null && x.DeletedAt < cutoff).ExecuteDeleteAsync();

            _logger.LogInformation("HardDeleteWorker: xoá {Count} bản ghi (cutoff={Cutoff})", deleted, cutoff);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "HardDeleteWorker gặp lỗi");
        }
    }
}
