using System.Net;
using System.Text.Json;


namespace BackendMada.Middleware
{
    public class ErroresGlobalesMiddleware
    {
        private readonly RequestDelegate _next;
        
        public ErroresGlobalesMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(context, e);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            // Configurar la respuesta HTTP
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            
            // Crear un objeto de respuesta personalizado
            var response = new
            {
                statusCode = context.Response.StatusCode,
                message = "Ocurrio un error en el servidor. ",
                details = exception.Message
            };
            
            // Serializar el objeto de respuesta
            var jsonResponse =JsonSerializer.Serialize(response);
            
            // Escribir la respuesta en el cuerpo de la respuesta HTTP
            return context.Response.WriteAsync(jsonResponse);
        }
    }
}

