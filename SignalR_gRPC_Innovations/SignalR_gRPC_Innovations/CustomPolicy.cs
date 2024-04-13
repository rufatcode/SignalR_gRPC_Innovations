using System;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

namespace SignalR_gRPC_Innovations
{
    public class CustomPolicy : IRateLimiterPolicy<string>
    {
        public CustomPolicy()
        {
        }

        public Func<OnRejectedContext, CancellationToken, ValueTask>? OnRejected => (context, cancellationToken) =>
        {
            return new();
        };

        public RateLimitPartition<string> GetPartition(HttpContext httpContext)
        {
            return RateLimitPartition.GetFixedWindowLimiter("", _ => new()
            {
                PermitLimit = 4,
                Window = TimeSpan.FromMinutes(20),
                QueueLimit = 2,
                QueueProcessingOrder = QueueProcessingOrder.OldestFirst,

            });
        }
    }
}

