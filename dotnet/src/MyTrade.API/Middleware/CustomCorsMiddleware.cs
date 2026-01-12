namespace MyTrade.API.Middleware
{
    public class CustomCorsMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomCorsMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var origin = context.Request.Headers["Origin"].ToString();
            var allowedOrigins = new[]
            {
            "http://localhost:3000",
            "http://localhost:3002",
            "https://localhost:3000",
            "https://localhost:3002",
            "https://localhost:3001",
            "https://localhost:3001"
        };

            if (!string.IsNullOrEmpty(origin) && allowedOrigins.Contains(origin))
            {
                context.Response.Headers.Append("Access-Control-Allow-Origin", origin);
                context.Response.Headers.Append("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");
                context.Response.Headers.Append("Access-Control-Allow-Headers", "Content-Type, Authorization");
                context.Response.Headers.Append("Access-Control-Allow-Credentials", "true");

                if (context.Request.Method == "OPTIONS")
                {
                    context.Response.StatusCode = 200;
                    return;
                }
            }

            await _next(context);
        }
    }
}
