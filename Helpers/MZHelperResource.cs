using System.Resources;

namespace MegaZord.Framework.Helpers {
    public class MZHelperResource {
        public static string GetValueFromResource<T>(string key) where T : class {
            var rm = new ResourceManager(typeof(T));
            return rm.GetString(key);
        }
    }
}
