namespace Capstone.Filters;

using Microsoft.AspNetCore.Mvc.Filters;

public class ResponseTimeFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var startTime = DateTime.UtcNow;
        
        var result = await next();
        
        var endTime = DateTime.UtcNow;
        var responseTime = (endTime - startTime).TotalMilliseconds;
        Console.WriteLine($"Response Time: {responseTime} ms");
    }
}