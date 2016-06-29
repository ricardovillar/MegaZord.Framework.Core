using Microsoft.AspNetCore.Http;

namespace MegaZord.Framework.Helpers {
    public static class MZHelperHTTP {
        private static IHttpContextAccessor HttpContextAccessor;
        public static void Configure(IHttpContextAccessor httpContextAccessor) {
            HttpContextAccessor = httpContextAccessor;
        }

        public static HttpContext HttpContext {
            get {
                return HttpContextAccessor.HttpContext;
            }
        }
    }

}
