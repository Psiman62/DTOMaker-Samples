using FluentAssertions;
using MessagePack;

namespace TodoListDTOs.Tests
{
    [TestClass]
    public class DTORegressionTests
    {
        [TestMethod]
        public void Roundtrip_DTO()
        {
            var orig = new DTOs.MyTodoList
            {
                Id = 123
            };
            ReadOnlyMemory<byte> buffer = orig.Block;

            var copy = new DTOs.MyTodoList(buffer.Span);
            copy.Id.Should().Be(orig.Id);
            //todo copy.Equals(orig).Should().BeTrue();
        }

        [TestMethod]
        public void Roundtrip_MessagePack()
        {
            var orig = new TodoListDTOs.MessagePack.MyTodoList
            {
                Id = 123
            };

            ReadOnlyMemory<byte> buffer = MessagePackSerializer.Serialize<TodoListDTOs.MessagePack.MyTodoList>(orig);

            var copy = MessagePackSerializer.Deserialize<TodoListDTOs.MessagePack.MyTodoList>(buffer);

            copy.Id.Should().Be(orig.Id);
            //todo copy.Equals(orig).Should().BeTrue();
        }
    }
}