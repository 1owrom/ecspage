using System;
using Microsoft.Extensions.DependencyInjection;

namespace ecspage.Bootstrap
{
    public static class AppHost
    {
        public static IServiceProvider Services { get; private set; }

        public static void Init(IServiceProvider provider) => Services = provider;

        public static T Get<T>() where T : notnull => Services.GetRequiredService<T>();
    }
}
