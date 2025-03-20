using PWA_PN.Models;

namespace PWA_PN.Services
{
    public interface IPushNotificationsQueue
    {
        Task QueueNotificationAsync(PushNotification notification);
    }

    public class PushNotificationsQueue : IPushNotificationsQueue
    {
        private readonly IPushNotificationsService _pushNotificationsService;

        public PushNotificationsQueue(IPushNotificationsService pushNotificationsService)
        {
            _pushNotificationsService = pushNotificationsService;
        }

        public async Task QueueNotificationAsync(PushNotification notification)
        {
            await _pushNotificationsService.SendNotificationAsync(notification);
        }
    }
}
