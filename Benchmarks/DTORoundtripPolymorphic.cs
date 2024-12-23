using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Order;
using MessagePack;

namespace Benchmarks
{
    [SimpleJob(RuntimeMoniker.Net90)]
    [MemoryDiagnoser]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    public class DTORoundtripPolymorphic
    {
        [Benchmark(Baseline = true)]
        public int Roundtrip_Polymorphic_MessagePack()
        {
            var dto = new Benchmarks.MessagePack.Rectangle()
            {
                Length = 3.0D,
                Height = 2.0D,
            };
            dto.Freeze();
            var buffer = MessagePackSerializer.Serialize<Benchmarks.MessagePack.Polygon>(dto);
            var copy = MessagePackSerializer.Deserialize<Benchmarks.MessagePack.Polygon>(buffer, out int bytesRead);
            dto.Freeze();
            return 0;
        }

        [Benchmark]
        public int Roundtrip_Polymorphic_MemBlocks()
        {
            var dto = new Benchmarks.MemBlocks.Rectangle()
            {
                Length = 3.0D,
                Height = 2.0D,
            };
            dto.Freeze();
            var buffers = dto.GetBuffers();
            string entityId = dto.GetEntityId();
            var copy = Benchmarks.MemBlocks.Polygon.CreateFrom(entityId, buffers);
            dto.Freeze();
            return 0;
        }

    }
}
