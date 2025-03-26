
using ServerSide.Services;

namespace ServerSide.Handlers
{
    public class ReminderBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<ReminderBackgroundService> _logger;

        public ReminderBackgroundService(IServiceProvider serviceProvider, ILogger<ReminderBackgroundService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var reminderService = scope.ServiceProvider.GetRequiredService<PhonSenderService>();
                         await reminderService.SendReminder();
                        _logger.LogInformation("تم ارسال التذكيرات");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "حدث خطأ أثناء إرسال التذكيرات.");
                }

                await Task.Delay(TimeSpan.FromSeconds(55), stoppingToken);
            }
        }
    }
}
