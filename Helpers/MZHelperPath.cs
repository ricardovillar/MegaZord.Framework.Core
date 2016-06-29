using Microsoft.AspNetCore.Hosting;

namespace MegaZord.Framework.Helpers {
    public static class MZHelperPath {
        private static IHostingEnvironment hostEnvironment;

        public static void Configure(IHostingEnvironment _httpContextAccessor) {
            hostEnvironment = _httpContextAccessor;
        }

        public static string RootDirectory { get { return hostEnvironment.ContentRootPath; } }
        public static string GetwwwRoot { get { return hostEnvironment.WebRootPath; } }

    }


}
