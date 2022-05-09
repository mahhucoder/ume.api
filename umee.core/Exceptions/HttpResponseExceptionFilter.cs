using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace umee.core.Exceptions
{
    public class HttpResponseExceptionFilter : Microsoft.AspNetCore.Mvc.Filters.IActionFilter, IOrderedFilter
    {
        public int Order => int.MaxValue - 10;

        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if(context.Exception != null)
            {
                if (context.Exception is UMEEException MahhuException)
                {
                    context.Result = new ObjectResult(MahhuException.Data)
                    {
                        StatusCode = 400
                    };

                    context.ExceptionHandled = true;
                }
                else
                {
                    var result = new
                    {
                        devMsg = context.Exception.Message,
                        userMsg = Properties.Resources.CodeError,
                        data = ""
                    };

                    context.Result = new ObjectResult(result)
                    {
                        StatusCode = 500
                    };

                    context.ExceptionHandled = true;
                }
            }
        }
    }
}
