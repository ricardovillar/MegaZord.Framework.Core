using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;


namespace MegaZord.Framework.MVC.Filter {
    public abstract class MZModelStateTempDataTransfer : ActionFilterAttribute {
        protected static readonly string Key = typeof(MZModelStateTempDataTransfer).FullName;
    }

    public class LMExportModelStateToTempData : MZModelStateTempDataTransfer {
        public override void OnActionExecuted(ActionExecutedContext filterContext) {
            Controller controller = filterContext.Controller as Controller;
            if (controller != null) {
                if (!controller.ViewData.ModelState.IsValid) {
                    if ((filterContext.Result is RedirectResult) || (filterContext.Result is RedirectToRouteResult)) {
                        controller.TempData[Key] = controller.ViewData.ModelState;
                    }
                }
            }
            base.OnActionExecuted(filterContext);
        }
    }

    public class LMImportModelStateFromTempData : MZModelStateTempDataTransfer {
        public override void OnActionExecuted(ActionExecutedContext filterContext) {

            Controller controller = filterContext.Controller as Controller;
            if (controller != null) {


                var modelState = controller.TempData[Key] as ModelStateDictionary;

                if (modelState != null) {
                    //Only Import if we are viewing
                    if (filterContext.Result is ViewResult) {
                        controller.ViewData.ModelState.Merge(modelState);
                    }
                    else {
                        //Otherwise remove it.
                        controller.TempData.Remove(Key);
                    }
                }
            }

            base.OnActionExecuted(filterContext);
        }
    }
}
