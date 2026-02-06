using System.Net;
using System.Text.Json;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;//call the function that will process this http request
    

    public ExceptionHandlingMiddleware( RequestDelegate next)
    {
        
        _next =next;
    }
    private static  Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        
        var response = context.Response;

        response.ContentType ="application/json";

        response.StatusCode = exception switch
        {
            InvalidCalculationException => StatusCodes.Status422UnprocessableEntity,
            _ => StatusCodes.Status500InternalServerError,

        };
        //create a payload object that contains error details so that it can be sent back to the client
        var payload = new 
        {
            error= exception.GetType().Name,//this will give the type of exception 
            details=exception.Message //this will give the message associated with the exception
        };

        return response.WriteAsync(JsonSerializer.Serialize(payload));//serialize the payload object to JSON and write it to the response body
    }

    public async Task InvokeAsync(HttpContext context)// context =request/ response
    {
        try
        {
            await _next(context);
        }
        catch(Exception ex)
        {
            await HandleExceptionAsync(context, ex);//call the method to handle the exception
        }
        
    }
}