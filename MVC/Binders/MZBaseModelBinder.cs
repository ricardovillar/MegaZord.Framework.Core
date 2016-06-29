using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MegaZord.Framework.MVC.Binders {
    public abstract class MZBaseModelBinder<T> : IModelBinder {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext) {
            var cultureCookie = controllerContext.HttpContext.Request.Cookies["culture"];

            Culture = "en-US";
            
            if (cultureCookie != null)
                Culture = cultureCookie;

            T value;

            var valueProvider = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            if (valueProvider == null)
                return null;

            if (String.IsNullOrEmpty(valueProvider.FirstValue))
                return null;

            if (TryParse(valueProvider.FirstValue, out value)) {
                return value;
            }

            bindingContext.ModelState.AddModelError(bindingContext.ModelName, ErrorMessage);

            return null;
        }

        protected string Culture { get; private set; }

        protected abstract bool TryParse(string s, out T result);

        public Task BindModelAsync(ModelBindingContext bindingContext) {
            throw new NotImplementedException();
        }

        protected abstract string ErrorMessage { get; }
    }
}
