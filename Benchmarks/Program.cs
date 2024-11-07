using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using DTOMaker.Models;
using MessagePack;
using System;

namespace Benchmarks
{
    [Entity]
    [EntityLayout(LayoutMethod.SequentialV1)]
    public interface IMyDTO
    {
        [Member(1)] bool Field01 { get; }
        [Member(2)][MemberEndian(false)] double Field02LE { get; }
        [Member(3)][MemberEndian(true)] double Field02BE { get; }
        [Member(4)] Guid Field03 { get; }
    }

    public enum ValueKind
    {
        Bool,
        DoubleLE,
        DoubleBE,
        Guid,
        // todo strings
    }

    //[SimpleJob(RuntimeMoniker.Net481)]
    [SimpleJob(RuntimeMoniker.Net80)]
    [MemoryDiagnoser]
    public class DTORoundtrips
    {
        [Params(ValueKind.Bool, ValueKind.DoubleLE, ValueKind.DoubleBE, ValueKind.Guid)]
        public ValueKind Kind;

        private static readonly Guid guidValue = new("cc8af561-5172-43e6-8090-5dc1b2d02e07");

        private MessagePack.MyDTO MakeMyDTO_MessagePack(ValueKind id)
        {
            var dto = new MessagePack.MyDTO();
            switch (Kind)
            {
                case ValueKind.Bool:
                    dto.Field01 = true;
                    break;
                case ValueKind.DoubleLE:
                    dto.Field02LE = Double.MaxValue;
                    break;
                case ValueKind.DoubleBE:
                    dto.Field02BE = Double.MaxValue;
                    break;
                case ValueKind.Guid:
                    dto.Field03 = guidValue;
                    break;
                default:
                    break;
            }
            return dto;
        }

        private MemBlocks.MyDTO MakeMyDTO_MemBlocks(ValueKind id)
        {
            var dto = new MemBlocks.MyDTO();
            switch (Kind)
            {
                case ValueKind.Bool:
                    dto.Field01 = true;
                    break;
                case ValueKind.DoubleLE:
                    dto.Field02LE = Double.MaxValue;
                    break;
                case ValueKind.DoubleBE:
                    dto.Field02BE = Double.MaxValue;
                    break;
                default:
                    break;
            }
            return dto;
        }

        public DTORoundtrips()
        {
        }

        [Benchmark(Baseline = true)]
        public int Roundtrip_MessagePack()
        {
            var dto = MakeMyDTO_MessagePack(Kind);
            dto.Freeze();
            ReadOnlyMemory<byte> buffer = MessagePackSerializer.Serialize<MessagePack.MyDTO>(dto);
            var copy = MessagePackSerializer.Deserialize<MessagePack.MyDTO>(buffer, out int bytesRead);
            dto.Freeze();
            return buffer.Length;
        }

        [Benchmark]
        public int Roundtrip_MemBlocks()
        {
            var dto = MakeMyDTO_MemBlocks(Kind);
            dto.Freeze();
            var buffer = dto.Block;
            var copy = new MemBlocks.MyDTO(buffer);
            return buffer.Length;
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<DTORoundtrips>();
        }
    }
}
