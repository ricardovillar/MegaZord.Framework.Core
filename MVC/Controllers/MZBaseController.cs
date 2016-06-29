using MegaZord.Framework.MVC.Filter;
using MegaZord.Framework.Helpers;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;

namespace MegaZord.Framework.MVC.Controllers {
    [MZHandleError]
    [MZShowClientMessage]
    public class MZBaseController : Controller {

        protected void ShowError(string msg) {
            this.TempData["LMError"] = msg;
        }
        protected void ShowSucess(string msg) {
            this.TempData["LMSucess"] = msg;
        }

        
        
        //}
        /// <summary>
        /// Retorna um objeto serializado em JSON
        /// </summary>
        /// <typeparam name="T">Tipo do Objeto a ser serializado</typeparam>
        /// <param name="obj">Objeto a ser serializado</param>
        /// <returns>String contendo o objeto</returns>
        protected string Serialize<T>(T obj, CultureInfo culture = null) where T : class {
            if (culture == null)
                culture = new CultureInfo("en-US");
            return MZHelperSerialize.Serialize<T>(obj, culture);

        }

        /// <summary>
        /// Deserializa um objeto
        /// </summary>
        /// <typeparam name="T">Tipo do OBjecto</typeparam>
        /// <param name="jsonObject">String Json do OBjeto</param>
        /// <returns>Objeto</returns>
        protected T Deserialize<T>(string jsonObject, CultureInfo culture = null) where T : class {
            if (culture == null)
                culture = new CultureInfo("en-US");
            return MZHelperSerialize.Deserialize<T>(jsonObject, culture);
        }


    }
}
