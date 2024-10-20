using DTOMaker.Runtime;
using FluentAssertions;
using MessagePack;

namespace TodoListDTOs.Tests
{
    [TestClass]
    public class DTORegressionTests
    {
        [TestMethod]
        public void Roundtrip_MemBlocks_Explicit()
        {
            var orig = new MemBlocks.AllTypesExplicit
            {
                Field01 = true,
                Field08 = 123
            };
            orig.Freeze();

            ReadOnlyMemory<byte> buffer = orig.Block;

            var copy = new MemBlocks.AllTypesExplicit(buffer);

            copy.IsFrozen().Should().BeTrue();
            copy.Field01.Should().Be(orig.Field01);
            copy.Field08.Should().Be(orig.Field08);
            //todo copy.Equals(orig).Should().BeTrue();
        }

        [TestMethod]
        public void Roundtrip_MessagePack()
        {
            var orig = new MessagePack.AllTypesExplicit
            {
                Field01 = true,
                Field08 = 123
            };
            orig.Freeze();

            ReadOnlyMemory<byte> buffer = MessagePackSerializer.Serialize<TodoListDTOs.MessagePack.AllTypesExplicit>(orig);

            var copy = MessagePackSerializer.Deserialize<TodoListDTOs.MessagePack.AllTypesExplicit>(buffer);
            copy.Freeze();

            copy.Field01.Should().Be(orig.Field01);
            copy.Field08.Should().Be(orig.Field08);
            //todo copy.Equals(orig).Should().BeTrue();
        }
    }
}