using PWA_PN.Models;
using System.Collections.Concurrent;

namespace PWA_PN.Services
{
    public interface IPushSubscriptionStore
    {
        Task<string> StoreSubscriptionAsync(PushSubscription subscription);

        Task<PushSubscription> GetSubscriptionAsync(string id);

        Task DeleteSubscriptionAsync(string id);

        Task<IReadOnlyCollection<PushSubscription>> GetAllSubscriptionsAsync();

        Task<IReadOnlyCollection<string>> GetAllSubscriptionIdsAsync();
    }

    public class InMemoryPushSubscriptionStore : IPushSubscriptionStore
    {
        private readonly ConcurrentDictionary<string, PushSubscription> _subscriptions = new ConcurrentDictionary<string, PushSubscription>();

        public Task<string> StoreSubscriptionAsync(PushSubscription subscription)
        {
            string id = Guid.NewGuid().ToString();
            _subscriptions.TryAdd(id, subscription);

            return Task.FromResult(id);
        }

        public Task<PushSubscription> GetSubscriptionAsync(string id)
        {
            if (_subscriptions.TryGetValue(id, out PushSubscription? subscription))
            {
                return Task.FromResult(subscription);
            }

            return Task.FromResult<PushSubscription>(null!);
        }

        public Task DeleteSubscriptionAsync(string id)
        {
            _subscriptions.TryRemove(id, out _);

            return Task.CompletedTask;
        }

        public Task<IReadOnlyCollection<PushSubscription>> GetAllSubscriptionsAsync()
        {
            return Task.FromResult<IReadOnlyCollection<PushSubscription>>(_subscriptions.Values.ToList());
        }

        public Task<IReadOnlyCollection<string>> GetAllSubscriptionIdsAsync()
        {
            return Task.FromResult<IReadOnlyCollection<string>>(_subscriptions.Keys.ToList());
        }
    }
}
