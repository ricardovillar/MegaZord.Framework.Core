using static System.Net.WebUtility;
using static MegaZord.Framework.Helpers.MZHelperSerialize;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Html;

public static class MZExtensionMethods {

    private class SummaryError {
        public SummaryError() { }
        public SummaryError(string valor) {
            this.Mensagem = valor;
        }
        public string Mensagem { get; set; }
    }

    public static HtmlString ToMvcHtmlString(this HtmlString htmlString) {
        if (htmlString != null) {
             
            return new HtmlString(HtmlDecode(htmlString.ToString()));
        }
        return null;
    }

    public static HtmlString LMValidationSummary(this HtmlHelper helper, string validationMessage = "") {
        const string scriptBase = @"<script type='text/javascript'>
                                        $(document).ready(function() {{
                                            ShowSummaryModal('{0}');
                                        }});
                                    </script>";

        //string retVal = "";
        if (helper.ViewData.ModelState.IsValid)
            return new HtmlString("");


        var lista = new List<SummaryError>();
        foreach (var key in helper.ViewData.ModelState.Keys) {
            foreach (var err in helper.ViewData.ModelState[key].Errors) {
                
                var msgHTML = HtmlEncode(err.ErrorMessage);
                lista.Add(new SummaryError(msgHTML));
            }

        }

        var errorSerialized = Serialize(lista);
        var retorno = string.Format(scriptBase, errorSerialized);


        return new HtmlString(retorno);
    }

    public static IOrderedQueryable<TSource> OrderBy<TSource>(
       this IEnumerable<TSource> query, string propertyName) {
        var entityType = typeof(TSource);

        //Create x=>x.PropName
        var propertyInfo = entityType.GetProperty(propertyName);
        ParameterExpression arg = Expression.Parameter(entityType, "x");
        MemberExpression property = Expression.Property(arg, propertyName);
        var selector = Expression.Lambda(property, new ParameterExpression[] { arg });

        //Get System.Linq.Queryable.OrderBy() method.
        var enumarableType = typeof(System.Linq.Queryable);
        var method = enumarableType.GetMethods()
             .Where(m => m.Name == "OrderBy" && m.IsGenericMethodDefinition)
             .Where(m => {
                 var parameters = m.GetParameters().ToList();
                 //Put more restriction here to ensure selecting the right overload                
                 return parameters.Count == 2;//overload that has 2 parameters
             }).Single();
        //The linq's OrderBy<TSource, TKey> has two generic types, which provided here
        MethodInfo genericMethod = method
             .MakeGenericMethod(entityType, propertyInfo.PropertyType);

        /*Call query.OrderBy(selector), with query and selector: x=> x.PropName
          Note that we pass the selector as Expression to the method and we don't compile it.
          By doing so EF can extract "order by" columns and generate SQL for it.*/
        var newQuery = (IOrderedQueryable<TSource>)genericMethod
             .Invoke(genericMethod, new object[] { query, selector });
        return newQuery;
    }
}