using System;
using Microsoft.Extensions.DependencyInjection;

namespace MegaZord.Framework.Helpers {
    public static class MZHelperInjection {
        private static IServiceProvider _serviceProvider;
        public static void Configure(IServiceCollection services) {
            _serviceProvider = services.BuildServiceProvider();
        }
        private static T Get<T>() {
            return _serviceProvider.GetService<T>();
        }
        
    }
}
