namespace Capstone.Filters;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

public class GlobalResponseFilter : IAsyncResultFilter
{
    public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        var result = context.Result;

        if (result is ObjectResult objectResult)
        {
            if (objectResult.Value != null)
            {
                var statusCode = objectResult.StatusCode ?? 200;
                var success = statusCode >= 200 && statusCode < 300;
                
                var message = statusCode switch
                {
                    200 => "Success",
                    201 => "Resource created successfully",
                    204 => "Operation completed successfully",
                    400 => "Bad request",
                    404 => "Resource not found",
                    500 => "Internal server error",
                    _ => success ? "Success" : "Error occurred"
                };

                var response = new
                {
                    success,
                    message,
                    data = objectResult.Value,
                    trace_id = context.HttpContext.TraceIdentifier,
                    statusCode,
                };

                objectResult.Value = response;
            }
            else
            {
                context.Result = new NotFoundObjectResult(new 
                { 
                    success = false,
                    message = "Resource not found.",
                    data = (object?)null,
                    trace_id = context.HttpContext.TraceIdentifier,
                    statusCode = 404
                });
            }
        }

        await next();
    }
}