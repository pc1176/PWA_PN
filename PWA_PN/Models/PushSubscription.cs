namespace PWA_PN.Models
{
    public class PushSubscription
    {
        public string Endpoint { get; set; } = string.Empty;

        public DateTime? ExpirationTime { get; set; }

        public PushSubscriptionKeys Keys { get; set; } = new PushSubscriptionKeys();
    }

    public class PushSubscriptionKeys
    {
        public string P256dh { get; set; } = string.Empty;

        public string Auth { get; set; } = string.Empty;
    }

    public class PushNotification
    {
        public string Title { get; set; } = string.Empty;

        public string Body { get; set; } = string.Empty;

        public string Icon { get; set; } = string.Empty;

        public string Badge { get; set; } = string.Empty;

        public int[] VibrationPattern { get; set; } = Array.Empty<int>();

        public Dictionary<string, object>? Data { get; set; }

        public Dictionary<string, object>? Actions { get; set; }
    }
}
