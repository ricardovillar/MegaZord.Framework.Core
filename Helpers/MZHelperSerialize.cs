using Newtonsoft.Json;
using System.Globalization;

namespace MegaZord.Framework.Helpers {
    public static class MZHelperSerialize {
        /// <summary>
        /// Retorna um objeto serializado em JSON
        /// </summary>
        /// <typeparam name="T">Tipo do Objeto a ser serializado</typeparam>
        /// <param name="obj">Objeto a ser serializado</param>
        /// <returns>String contendo o objeto</returns>
        public static string Serialize<T>(T obj) where T : class {
            return Serialize<T>(obj, new CultureInfo("en-US"));

        }

        /// <summary>
        /// Retorna um objeto serializado em JSON
        /// </summary>
        /// <typeparam name="T">Tipo do Objeto a ser serializado</typeparam>
        /// <typeparam name="culture">Cultura para utilizar</typeparam>
        /// <param name="obj">Objeto a ser serializado</param>
        /// <returns>String contendo o objeto</returns>
        public static string Serialize<T>(T obj, CultureInfo culture) where T : class {
            var settings = GetSettings(culture);
            return JsonConvert.SerializeObject(obj, settings);

        }

        /// <summary>
        /// Deserializa um objeto
        /// </summary>
        /// <typeparam name="T">Tipo do OBjecto</typeparam>
        /// <param name="jsonObject">String Json do OBjeto</param>
        /// <returns>Objeto</returns>
        public static T Deserialize<T>(string jsonObject) where T : class {
            return Deserialize<T>(jsonObject, new CultureInfo("en-US"));
        }

        /// <summary>
        /// Deserializa um objeto usando a cultura
        /// </summary>
        /// <typeparam name="T">Tipo do OBjecto</typeparam>
        /// <typeparam name="culture">Cultura para utilizar</typeparam>
        /// <param name="jsonObject">String Json do OBjeto</param>
        /// <returns>Objeto</returns>
        public static T Deserialize<T>(string jsonObject, CultureInfo culture) where T : class {
            var settings = GetSettings(culture);
            return JsonConvert.DeserializeObject<T>(jsonObject, settings);
        }

        private static JsonSerializerSettings GetSettings(CultureInfo culture) {
            return new JsonSerializerSettings() {
                Culture = culture
            };
        }

    }
}
