using DTOMaker.Runtime;
using FluentAssertions;
using MessagePack;
using System.Linq;

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

            string.Join("-", buffer.ToArray().Select(b => b.ToString("X2"))).Should().Be(
                "01-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-"+"" +
                "7B-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-"+
                "00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-"+
                "00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-"+
                "00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-"+
                "00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-"+
                "00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-"+
                "00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00");

            var copy = new MemBlocks.AllTypesExplicit(buffer);
            copy.Freeze();

            copy.IsFrozen().Should().BeTrue();
            copy.Field01.Should().Be(orig.Field01);
            copy.Field08.Should().Be(orig.Field08);
            //todo copy.Equals(orig).Should().BeTrue();
        }

        [TestMethod]
        public void Roundtrip_MemBlocks_Sequential()
        {
            var orig = new MemBlocks.AllTypesSequential
            {
                Field01 = true,
                Field08 = 123,
            };
            IAllTypesSequential origIntf = orig;
            origIntf.Field16_StringUTF8 = "abcdef";

            orig.Freeze();

            ReadOnlyMemory<byte> buffer = orig.Block;
            string.Join("-", buffer.ToArray().Select(b => b.ToString("X2"))).Should().Be(
                "01-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-" +
                "7B-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-" +
                "00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-" +
                "00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-" +
                "00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-" +
                "00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-" +
                "61-62-63-64-65-66-00-00-00-00-00-00-00-00-00-00-" +
                "00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-" +
                "00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-" +
                "00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-" +
                "00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-" +
                "00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-" +
                "06-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-" +
                "00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-" +
                "00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-" +
                "00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00");
            var copy = new MemBlocks.AllTypesSequential(buffer);
            copy.Freeze();
            IAllTypesSequential copyIntf = copy;

            copy.IsFrozen().Should().BeTrue();
            copy.Field01.Should().Be(orig.Field01);
            copy.Field08.Should().Be(orig.Field08);
            copy.Field16_Length.Should().Be(orig.Field16_Length);
            copyIntf.Field16_StringUTF8.Should().Be(origIntf.Field16_StringUTF8);
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

            string.Join("-", buffer.ToArray().Select(b => b.ToString("X2"))).Should().Be(
                "DC-00-10-C0-C3-00-00-00-00-00-00-7B-CA-00-00-00-" +
                "00-00-00-CB-00-00-00-00-00-00-00-00-D9-24-30-30-" +
                "30-30-30-30-30-30-2D-30-30-30-30-2D-30-30-30-30-" +
                "2D-30-30-30-30-2D-30-30-30-30-30-30-30-30-30-30-" +
                "30-30-A1-30-00");

            var copy = MessagePackSerializer.Deserialize<TodoListDTOs.MessagePack.AllTypesExplicit>(buffer);
            copy.Freeze();

            copy.Field01.Should().Be(orig.Field01);
            copy.Field08.Should().Be(orig.Field08);
            //todo copy.Equals(orig).Should().BeTrue();
        }
    }
}