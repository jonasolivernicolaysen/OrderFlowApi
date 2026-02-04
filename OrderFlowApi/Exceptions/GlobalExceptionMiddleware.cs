namespace OrderFlowApi.Exceptions
{
    // global exception middleware written by chatgpt
    // this removes the need for try catch blocks in controllers so controllers stay slimmer
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleException(context, ex);
            }
        }

        public static Task HandleException(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json"; 

            context.Response.StatusCode = ex switch
            {
                ProductNotFoundException => StatusCodes.Status404NotFound,
                OrderNotFoundException => StatusCodes.Status404NotFound,
                UserNotAuthorizedException => StatusCodes.Status403Forbidden,
                BadProductListingException => StatusCodes.Status400BadRequest,
                BadOrderException => StatusCodes.Status400BadRequest,
                InvalidOrderStateException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };

            var response = new
            {
                error = ex.Message
            };

            return context.Response.WriteAsJsonAsync(response);
        }
    }
}
