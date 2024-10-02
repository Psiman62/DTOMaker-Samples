using FluentAssertions;
using MessagePack;

namespace TodoListDTOs.Tests
{
    [TestClass]
    public class DTORegressionTests
    {
        [TestMethod]
        public void Roundtrip_MemBlocks()
        {
            var orig = new MemBlocks.MyTodoList
            {
                Id = 123L,
                Code = 123,
                HasValue = true,
                Value = 123.456D
            };
            ReadOnlyMemory<byte> buffer = orig.Block;

            var copy = new MemBlocks.MyTodoList(buffer.Span);
            copy.Id.Should().Be(orig.Id);
            copy.Code.Should().Be(orig.Code);
            copy.HasValue.Should().Be(orig.HasValue);
            copy.Value.Should().Be(orig.Value);
            //todo copy.Equals(orig).Should().BeTrue();
        }

        [TestMethod]
        public void Roundtrip_MessagePack()
        {
            var orig = new TodoListDTOs.MessagePack.MyTodoList
            {
                Id = 123L,
                Code = 123,
                HasValue = true,
                Value = 123.456D
            };

            ReadOnlyMemory<byte> buffer = MessagePackSerializer.Serialize<TodoListDTOs.MessagePack.MyTodoList>(orig);

            var copy = MessagePackSerializer.Deserialize<TodoListDTOs.MessagePack.MyTodoList>(buffer);
            copy.Id.Should().Be(orig.Id);
            copy.Code.Should().Be(orig.Code);
            copy.HasValue.Should().Be(orig.HasValue);
            copy.Value.Should().Be(orig.Value);
            //todo copy.Equals(orig).Should().BeTrue();
        }
    }
}