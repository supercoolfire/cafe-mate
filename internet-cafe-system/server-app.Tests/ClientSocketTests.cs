using Xunit;
using CafeMate.Shared;

namespace server_app.Tests
{
    public class ClientSocketTests
    {
        [Fact]
        public void CanSerializeClientMessage()
        {
            var msg = new Message { Type = "Ping", Content = "Test" };
            string json = MessageProtocol.Serialize(msg);
            var copy = MessageProtocol.Deserialize<Message>(json);

            Assert.Equal("Ping", copy.Type);
            Assert.Equal("Test", copy.Content);
        }
    }
}
