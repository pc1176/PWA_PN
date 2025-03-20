using PWA_PN.Models;
using System.Text.Json;
using WebPush;

namespace PWA_PN.Services
{
    public interface IPushNotificationsService
    {
        Task SendNotificationAsync(PushNotification notification);
    }

    public class PushNotificationsService : IPushNotificationsService
    {
        private readonly IPushSubscriptionStore _subscriptionStore;
        private readonly IVapidTokensStorage _vapidTokensStorage;
        private readonly ILogger<PushNotificationsService> _logger;

        public PushNotificationsService(IPushSubscriptionStore subscriptionStore, IVapidTokensStorage vapidTokensStorage, ILogger<PushNotificationsService> logger)
        {
            _subscriptionStore = subscriptionStore;
            _vapidTokensStorage = vapidTokensStorage;
            _logger = logger;
        }

        public async Task SendNotificationAsync(PushNotification notification)
        {
            var subscriptions = await _subscriptionStore.GetAllSubscriptionsAsync();

            if (subscriptions.Count == 0)
            {
                _logger.LogInformation("No subscriptions to send notifications to");
                return;
            }

            var webPushClient = new WebPushClient();
            var vapidDetails = new VapidDetails("mailto:example@example.com", _vapidTokensStorage.PublicKey, _vapidTokensStorage.PrivateKey);

            string payload = JsonSerializer.Serialize(notification);

            foreach (var subscription in subscriptions)
            {
                try
                {
                    var pushSubscription = new WebPush.PushSubscription(
                        subscription.Endpoint,
                        subscription.Keys.P256dh,
                        subscription.Keys.Auth
                    );

                    await webPushClient.SendNotificationAsync(pushSubscription, payload, vapidDetails);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to send push notification");
                }
            }
        }
    }
}
