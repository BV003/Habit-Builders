namespace habitsBuilderBackEnd.Services
{
    //public class DailyTaskService : BackgroundService
    //{
    //    private readonly IServiceProvider _serviceProvider;
    //
    //    public DailyTaskService(IServiceProvider serviceProvider)
    //    {
    //        _serviceProvider = serviceProvider;
    //    }
    //
    //    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    //    {
    //        while (!stoppingToken.IsCancellationRequested)
    //        {
    //            var now = DateTime.Now;
    //            var nextRun = now.Date.AddDays(1).AddHours(0); // Next midnight
    //
    //            var delay = nextRun - now;
    //            if (delay.TotalMilliseconds <= 0)
    //            {
    //                delay = TimeSpan.Zero;
    //            }
    //
    //            await Task.Delay(delay, stoppingToken);
    //
    //            using (var scope = _serviceProvider.CreateScope())
    //            {
    //                var userDataService = scope.ServiceProvider.GetRequiredService<CardService>();
    //                await userDataService.UpdateCheckDaysAsync();
    //            }
    //        }
    //    }
    //}
}
