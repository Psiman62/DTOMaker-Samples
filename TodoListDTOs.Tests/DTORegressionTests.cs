using DataFac.Runtime;
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
            var orig = new TodoListDTOs.CSPoco.AllTypesExplicit()
            {
                Field01 = true,
                Field08 = 123
            };
            orig.Freeze();

            var transport1 = new MemBlocks.AllTypesExplicit(orig);
            transport1.Freeze();

            var buffers = transport1.GetBuffers();
            buffers.Length.Should().Be(1);

            string.Join("-", buffers.Span[0].ToArray().Select(b => b.ToString("X2"))).Should().Be(
                "01-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-"+"" +
                "7B-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-"+
                "00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-"+
                "00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-"+
                "00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-"+
                "00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-"+
                "00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-"+
                "00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00");

            var transport2 = new MemBlocks.AllTypesExplicit(buffers);
            transport2.Freeze();

            var copy = new CSPoco.AllTypesExplicit(transport2);
            copy.Freeze();

            copy.Field01.Should().Be(orig.Field01);
            copy.Field08.Should().Be(orig.Field08);
        }

        [TestMethod]
        public void Roundtrip_MemBlocks_Sequential()
        {
            var orig = new CSPoco.AllTypesSequential()
            {
                Field01 = true,
                Field08 = 123
            };
            IAllTypesSequential origIntf = orig;
            origIntf.Field16_StringUTF8 = "abcdef";
            orig.Freeze();

            var transport1 = new MemBlocks.AllTypesSequential(orig);
            transport1.Freeze();

            var buffers = transport1.GetBuffers();
            buffers.Length.Should().Be(1);

            string.Join("-", buffers.Span[0].ToArray().Select(b => b.ToString("X2"))).Should().Be(
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
            var transport2 = new MemBlocks.AllTypesSequential(buffers);
            transport2.Freeze();

            var copy = new CSPoco.AllTypesSequential(transport2);
            copy.Freeze();

            copy.IsFrozen().Should().BeTrue();
            copy.Field01.Should().Be(orig.Field01);
            copy.Field08.Should().Be(orig.Field08);
            copy.Field16_Length.Should().Be(orig.Field16_Length);

            IAllTypesSequential copyIntf = copy;
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