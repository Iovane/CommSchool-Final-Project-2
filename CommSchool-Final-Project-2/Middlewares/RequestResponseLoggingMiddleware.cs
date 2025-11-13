using System.Text;
using Serilog;

namespace CommSchool_Final_Project_2.Middlewares;

public class RequestResponseLoggingMiddleware
{
    private readonly RequestDelegate _next;

    public RequestResponseLoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            context.Request.EnableBuffering();
            var requestBody = await new StreamReader(context.Request.Body, Encoding.UTF8).ReadToEndAsync();
            context.Request.Body.Position = 0;

            Log.Information("Incoming Request: {Method} {Path} | Query: {Query} | Body: {Body}",
                context.Request.Method,
                context.Request.Path,
                context.Request.QueryString.Value,
                requestBody);

            var originalBodyStream = context.Response.Body;
            using var responseBody = new MemoryStream();
            context.Response.Body = responseBody;

            await _next(context);

            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var responseText = await new StreamReader(context.Response.Body).ReadToEndAsync();
            context.Response.Body.Seek(0, SeekOrigin.Begin);

            Log.Information("Outgoing Response: {StatusCode} | Body: {Body}",
                context.Response.StatusCode,
                responseText);

            await responseBody.CopyToAsync(originalBodyStream);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Unhandled exception occurred while processing request {Method} {Path}",
                context.Request.Method, context.Request.Path);

            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;

        var response = new
        {
            error = "An unexpected error occurred.",
            details = exception.Message
        };

        var json = System.Text.Json.JsonSerializer.Serialize(response);

        await context.Response.WriteAsync(json);
    }
}