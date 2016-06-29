using System;
using MegaZord.Framework.Compatible;
using System.Text;

namespace MegaZord.Framework.Helpers {
    public class MZHelperException {        
        public static string GetErrorMessage(Exception ex) {
            string retorno = string.Empty;
            if (ex != null) {
                if (ex is DbEntityValidationException) {
                    var strLogErro = new StringBuilder();
                    var e = (DbEntityValidationException)ex;
                    foreach (var eve in e.EntityValidationErrors) {
                        //CASTRO
                        //var separador = strLogErro.Length == 0 ? string.Empty : System.Environment.NewLine;
                        //strLogErro.AppendFormat("{2}Entidade do tipo \"{0}\" com estado \"{1}\" Possui os seguintes erros:", eve.Entry.Entity.GetType().Name, eve.Entry.State, separador);
                        //foreach (var ve in eve.ValidationErrors) {
                        //    strLogErro.AppendFormat(System.Environment.NewLine + "- Propriedade: \"{0}\", Erro: \"{1}\"", ve.PropertyName, ve.ErrorMessage);
                        //}
                        strLogErro.AppendLine(eve.ErrorMessage);
                    }
                    retorno = strLogErro.ToString();
                }
                else if (ex.InnerException != null) {
                    var isInnerEx = ex.InnerException;
                    while (isInnerEx != null && isInnerEx.InnerException != null) {
                        isInnerEx = isInnerEx.InnerException;
                    }
                    if (isInnerEx != null)
                        retorno = isInnerEx.Message;
                }
                else {
                    retorno = ex.Message;
                }
            }
            return retorno;
        }
    }
}
