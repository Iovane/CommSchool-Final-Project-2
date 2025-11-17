using System.Diagnostics;
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
        var traceId = Activity.Current?.Id ?? context.TraceIdentifier;
        var originalBodyStream = context.Response.Body;

        try
        {
            context.Request.EnableBuffering();
            var requestBody = await new StreamReader(context.Request.Body, Encoding.UTF8).ReadToEndAsync();
            context.Request.Body.Position = 0;

            Log.Information("Incoming Request: {Method} {Path} | Query: {Query} | Body: {Body}",
                context.Request.Method,
                context.Request.Path,
                traceId,
                context.Request.QueryString.Value,
                requestBody);

            using var responseBody = new MemoryStream();
            context.Response.Body = responseBody;

            await _next(context);

            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var responseText = await new StreamReader(context.Response.Body).ReadToEndAsync();
            context.Response.Body.Seek(0, SeekOrigin.Begin);

            Log.Information("Outgoing Response: {StatusCode} | Body: {Body}",
                context.Response.StatusCode,
                traceId,
                responseText);

            await responseBody.CopyToAsync(originalBodyStream);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Unhandled exception occurred | TraceId: {TraceId} | Method: {Method} | Path: {Path} | Message: {Message}",
                    traceId, 
                    context.Request.Method, 
                    context.Request.Path, 
                    ex.Message);

            context.Response.Body = originalBodyStream;
            context.Response.Clear();
            await HandleExceptionAsync(context, ex, traceId);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception, string traceId)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;

        var response = new
        {
            error = "An unexpected error occurred.",
            details = exception.Message,
            traceId = traceId
        };

        var json = System.Text.Json.JsonSerializer.Serialize(response);

        await context.Response.WriteAsync(json);
    }
}