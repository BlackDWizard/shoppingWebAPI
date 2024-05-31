using System.Net.WebSockets;
using System.Text;

namespace ShoppingWebAPI.Chat
{
    public class ChatService
    {
        private readonly List<WebSocket> _sockets = new();

        public async Task HandleWebSocketConnection(WebSocket socket)
        {
            _sockets.Add(socket);
            var buffer = new byte[1024 * 2];
            while (socket.State == WebSocketState.Open)
            {
                var result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), default);
                if (result.MessageType == WebSocketMessageType.Close)
                {
                    await socket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, default);
                    break;
                }

                foreach (var s in _sockets)
                {
                    var messageString = Encoding.UTF8.GetString(buffer[..result.Count]);
                    var bufferSend = new ArraySegment<byte>(Encoding.UTF8.GetBytes(messageString + "\t" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")));
                    await s.SendAsync(bufferSend, WebSocketMessageType.Text, true, default);
                }
            }
            _sockets.Remove(socket);
        }
    }
}
