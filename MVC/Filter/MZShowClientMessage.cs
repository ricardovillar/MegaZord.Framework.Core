using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using static MegaZord.Framework.Helpers.MZHelperString;

namespace MegaZord.Framework.MVC.Filter {
    public class MZShowClientMessage : ActionFilterAttribute {

        public override void OnActionExecuting(ActionExecutingContext context) {
            base.OnActionExecuting(context);
            

            Controller controller = context.Controller as Controller;
            if (controller != null) {


                var LMExceptionMessage = controller.TempData["LMExceptionMessage"];

                var LMError = controller.TempData["LMError"];

                var LMSucess = controller.TempData["LMSucess"];



                var msgToShow = string.Empty;
                var typeBooleanIsError = "true";
                if (LMExceptionMessage != null && !string.IsNullOrEmpty(LMExceptionMessage.ToString())) {
                    msgToShow = JavaScriptStringEncode(LMExceptionMessage.ToString());
                }
                else if (LMError != null && !string.IsNullOrEmpty(LMError.ToString())) {
                    msgToShow = JavaScriptStringEncode(LMError.ToString());
                }
                else if (LMSucess != null && !string.IsNullOrEmpty(LMSucess.ToString())) {
                    msgToShow = JavaScriptStringEncode(LMSucess.ToString());
                    typeBooleanIsError = "false";
                }


                if (!string.IsNullOrEmpty(msgToShow)) {

                    const string scriptBase = @"<script type='text/javascript'>
                                        $(document).ready(function() {{
                                            ShowModalAlert('{0}', {1});
                                        }});
                                    </script>";
                    var msg = JavaScriptStringEncode(msgToShow);
                    msgToShow = string.Format(scriptBase, msg, typeBooleanIsError);
                    var arrayMsg = GetBytes(msgToShow);
                    context.HttpContext.Response.Body.Write(arrayMsg, 0, arrayMsg.Length);

                }
            }

        }
    }
}
