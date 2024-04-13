using System;
using System.Threading.RateLimiting;
using Microsoft.AspNetCore.RateLimiting;

namespace SignalR_gRPC_Innovations
{
    public static class ServiceRegistration
    {
        public static void Registration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            #region Fiex Window Algoritms
            services.AddRateLimiter(options =>
            {
                options.AddFixedWindowLimiter("Fixed", _options =>
                {
                    _options.Window = TimeSpan.FromSeconds(10);
                    _options.PermitLimit = 4;
                    _options.QueueLimit = 3;
                    _options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                });
                /*
                10 saniyədə 10 sorğuya cavab verə biləcək və extra sorğulardan 3 nü gözləməyə alacaq
                digərlərini görməzdən gələcəy və 10 sorğunu qarşıladıqdan sonra gözləməyə alıqı 3 sorğunu köhnədən yeniyə
                doğru qarşılayacaq.
                */
            });

            #endregion
            #region Sliding Window
            services.AddRateLimiter(options =>
            {
                options.AddSlidingWindowLimiter("Sliding", _option =>
                {
                    _option.Window = TimeSpan.FromSeconds(10);
                    _option.PermitLimit = 4;
                    _option.QueueLimit = 2;
                    _option.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                    _option.SegmentsPerWindow = 2;//Bunun vasitəsi ilə 5 ci saniyədən sonra 4 limiti bitsə 2 sinidə extra işlədə biləcək.
                });
            });
            #endregion
            #region Token Bucket
            services.AddRateLimiter(options =>
            {
                options.AddTokenBucketLimiter("Token", _options =>
                {
                    _options.QueueLimit = 2;
                    _options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                    _options.TokenLimit = 4;
                    _options.TokensPerPeriod = 4;
                });
            });
            #endregion
            #region Concruency
            services.AddRateLimiter(options =>
            {
                options.AddConcurrencyLimiter("Concurency", _options =>
                {
                    _options.PermitLimit = 4;
                    _options.QueueLimit = 2;
                    _options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                });
                options.OnRejected = (context, cancellationToken) =>
                {
                    return new();
                };
            });
            //paralel olaraq 4 request baş verə bilər eyni zamanda və 2 sini gözləməyə alacaq 4 ü bitəndən sonra o 2 sini başladacaq və ondan elave gələn requestləri almayacaq.
            #endregion
            #region CustomPolicy
            services.AddRateLimiter(options =>
            {
                options.AddPolicy<string, CustomPolicy>("CustomPolicy");
            });
            #endregion

        }
    }
}

