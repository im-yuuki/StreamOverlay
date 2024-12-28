using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using StreamOverlay.Data;

namespace StreamOverlay.Core;

[ApiController]
[Route("/ws")]
public sealed class WebSocketController : ControllerBase
{
    private readonly ConcurrentDictionary<Guid, WebSocket> _chatListeners = new();

    [HttpGet("chat")]
    public async Task ConnectWebSocket()
    {
        if (HttpContext.WebSockets.IsWebSocketRequest) {
            using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
            var connectionId = Guid.NewGuid();
            _chatListeners.TryAdd(connectionId, webSocket);

            // just keep the connection open
            while (webSocket.State is WebSocketState.Open or WebSocketState.Connecting) {
                await Task.Delay(1000).ConfigureAwait(false);
            }

            _chatListeners.TryRemove(connectionId, out _);
        }
        else {
            HttpContext.Response.StatusCode = 400;
        }
    }

    public async Task BroadcastMessage(Message message)
    {
        foreach (var client in _chatListeners) {
            if (client.Value.State is not WebSocketState.Open) continue;
            var buffer = Encoding.UTF8.GetBytes(message.ToJson());

            await client.Value.SendAsync(
                new ArraySegment<byte>(buffer, 0, buffer.Length),
                WebSocketMessageType.Text,
                true,
                CancellationToken.None
                ).ConfigureAwait(false);
        }
    }
}