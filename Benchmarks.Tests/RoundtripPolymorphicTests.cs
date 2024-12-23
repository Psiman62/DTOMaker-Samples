namespace Benchmarks.Tests
{
    public class RoundtripPolymorphicTests
    {
        [Fact]
        public void Roundtrip_MessagePack()
        {
            var sut = new DTORoundtripPolymorphic();
            sut.Roundtrip_Polymorphic_MessagePack();
        }

        [Fact]
        public void Roundtrip_MemBlocks()
        {
            var sut = new DTORoundtripPolymorphic();
            sut.Roundtrip_Polymorphic_MemBlocks();
        }
    }
}