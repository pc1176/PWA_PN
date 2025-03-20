using Microsoft.AspNetCore.Mvc;
using PWA_PN.Models;
using PWA_PN.Services;

namespace PWA_PN.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PushNotificationsController : ControllerBase
    {
        private readonly IPushSubscriptionStore _subscriptionStore;
        private readonly IVapidTokensStorage _vapidTokensStorage;
        private readonly IPushNotificationsQueue _notificationsQueue;
        private readonly ILogger<PushNotificationsController> _logger;

        public PushNotificationsController(
            IPushSubscriptionStore subscriptionStore,
            IVapidTokensStorage vapidTokensStorage,
            IPushNotificationsQueue notificationsQueue,
            ILogger<PushNotificationsController> logger)
        {
            _subscriptionStore = subscriptionStore;
            _vapidTokensStorage = vapidTokensStorage;
            _notificationsQueue = notificationsQueue;
            _logger = logger;
        }

        [HttpGet("public-key")]
        public IActionResult GetPublicKey()
        {
            return Ok(new { publicKey = _vapidTokensStorage.PublicKey });
        }

        [HttpPost("subscriptions")]
        public async Task<IActionResult> StoreSubscription([FromBody] PushSubscription subscription)
        {
            string id = await _subscriptionStore.StoreSubscriptionAsync(subscription);
            _logger.LogInformation("Subscription stored with ID: {Id}", id);

            return Ok(new { id });
        }

        [HttpDelete("subscriptions/{id}")]
        public async Task<IActionResult> DeleteSubscription(string id)
        {
            await _subscriptionStore.DeleteSubscriptionAsync(id);
            _logger.LogInformation("Subscription deleted with ID: {Id}", id);

            return NoContent();
        }

        [HttpPost("notifications")]
        public async Task<IActionResult> SendNotification([FromBody] PushNotification notification)
        {
            await _notificationsQueue.QueueNotificationAsync(notification);
            _logger.LogInformation("Notification queued: {Title}", notification.Title);

            return Accepted();
        }
    }
}
