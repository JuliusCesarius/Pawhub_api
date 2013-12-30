using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pawhub_API.Models.Filters
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Formatting;
    using System.Web.Http.Filters;

    public class NotImplementedExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            context.Response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
            context.Response.Content = new System.Net.Http.ObjectContent<ResponseResult<Object>>(new ResponseResult<Object> { Result = context.Exception.GetBaseException().Message }, new JsonMediaTypeFormatter());
        }
    }
}