using System;
using Xunit;
using CafeMate.Server.Networking;
using CafeMate.Shared;

namespace server_app.Tests
{
    public class ServerSocketTests
    {
        [Fact]
        public void CanInstantiateServerSocket()
        {
            var server = new ServerSocket();
            Assert.NotNull(server);
        }

        [Fact]
        public void CanSerializeAndDeserializeMessage()
        {
            var msg = new Message { Type = "TEST", Content = "Hello" };
            string json = MessageProtocol.Serialize(msg);
            var copy = MessageProtocol.Deserialize<Message>(json);

            Assert.Equal("TEST", copy.Type);
            Assert.Equal("Hello", copy.Content);
        }
    }
}
