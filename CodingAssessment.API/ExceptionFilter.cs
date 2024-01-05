using CoddingAssesment.Application.Commands;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace CodingAssessment.API
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {

            Action handler = context.Exception switch
            {
                LateEventException or NotLunchTimeException => () => HandleException(context, HttpStatusCode.BadRequest),
                _ => () => HandleException(context, HttpStatusCode.InternalServerError)
            };

            handler();
        }

        private void HandleException(ExceptionContext context, HttpStatusCode statusCode)
        {
            context.Result = new StatusCodeResult((int)statusCode);
        }
    }
}
