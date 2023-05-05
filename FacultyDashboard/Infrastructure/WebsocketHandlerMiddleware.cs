using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FacultyDashboard.Infrastructure
{
    public class WebsocketHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        public WebsocketHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path == "/ws")
            {
                // After the client successfully establishes a connection with the server , The server will receive the messages sent by the client asynchronously , After receiving the message, it will execute Handle(WebsocketClient websocketClient) Medium do{}while; Until the client is disconnected 

                // Different clients send messages to the server to execute in the background do{}while; when ,websocketClient Arguments are different , It corresponds to the client one by one 

                // The same client sends messages to the server multiple times and executes in the background do{}while; when ,websocketClient The arguments are the same 

                if (context.WebSockets.IsWebSocketRequest)
                {
                    var webSocket = await context.WebSockets.AcceptWebSocketAsync();
                }
                else
                {
                    context.Response.StatusCode = 404;
                }
            }
            else
            {
                await _next(context);
            }
        }
    }
}