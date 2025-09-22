using Xunit;
using CafeMate.Shared;

namespace ServerApp.Tests
{
    public class MessageProtocolTests
    {
        [Fact]
        public void Serialize_ShouldReturnJsonString()
        {
            // Arrange
            var msg = new Message
            {
                Type = "HELLO",
                Content = "World"
            };

            // Act
            var json = MessageProtocol.Serialize(msg);

            // Assert
            Assert.Contains("\"Type\":\"HELLO\"", json);
            Assert.Contains("\"Content\":\"World\"", json);
        }

        [Fact]
        public void Deserialize_ShouldReturnMessageObject()
        {
            // Arrange
            string json = "{\"Type\":\"HELLO\",\"Content\":\"World\"}";

            // Act
            var msg = MessageProtocol.Deserialize<Message>(json);

            // Assert
            Assert.NotNull(msg);
            Assert.Equal("HELLO", msg.Type);
            Assert.Equal("World", msg.Content);
        }

        [Fact]
        public void RoundTrip_SerializeAndDeserialize_ShouldMatchOriginal()
        {
            // Arrange
            var original = new Message
            {
                Type = "PING",
                Content = "12345"
            };

            // Act
            var json = MessageProtocol.Serialize(original);
            var deserialized = MessageProtocol.Deserialize<Message>(json);

            // Assert
            Assert.Equal(original.Type, deserialized.Type);
            Assert.Equal(original.Content, deserialized.Content);
        }

        [Fact]
        public void Deserialize_InvalidJson_ShouldThrow()
        {
            // Arrange
            string invalidJson = "{ this is not valid json }";

            // Act & Assert
            Assert.Throws<System.Text.Json.JsonException>(() =>
                MessageProtocol.Deserialize<Message>(invalidJson)
            );
        }

        [Fact]
        public void Serialize_NullMessage_ShouldReturnValidJson()
        {
            // Arrange
            Message? msg = null;

            // Act
            var json = MessageProtocol.Serialize(msg);

            // Assert
            Assert.Equal("null", json); // System.Text.Json serializes null as "null"
        }
    }
}
