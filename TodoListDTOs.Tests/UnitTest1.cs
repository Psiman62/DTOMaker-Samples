using DTOMaker.Runtime;
using FluentAssertions;
using MessagePack;

namespace TodoListDTOs.Tests
{
    internal sealed class NullStore : IBlobStore
    {
        public ValueTask<Octets> LoadAsync(BlobId id, CancellationToken token) => new ValueTask<Octets>(Octets.Empty);
        public ValueTask<BlobId> SaveAsync(Octets content, CancellationToken token) => new ValueTask<BlobId>(new BlobId());
        public ValueTask<(bool, Octets?)> TryLoadAsync(BlobId id, CancellationToken token) => new ValueTask<(bool, Octets?)>((false, Octets.Empty));
    }

    [TestClass]
    public class DTORegressionTests
    {
        [TestMethod]
        public void Roundtrip_MemBlocks_Explicit()
        {
            var store = new NullStore();
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
            var store = new NullStore();
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