using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using DataFac.Memory;
using DTOMaker.Models;
using MemoryPack;
using MessagePack;
using System;

namespace Benchmarks
{
    [Entity]
    [EntityLayout(LayoutMethod.SequentialV1)]
    public interface IMyDTO
    {
        [Member(1)] bool Field01 { get; }
        [Member(2)][MemberLayout(0, false)] double Field02LE { get; }
        [Member(3)][MemberLayout(0, true)] double Field02BE { get; }
        [Member(4)] Guid Field03 { get; }
    }

    public sealed class NetStruxMyDTO : IMyDTO
    {
        private BlockB064 _block;

        public NetStruxMyDTO() { }
        public bool TryRead(ReadOnlySpan<byte> source) => _block.TryRead(source);
        public bool TryWrite(Span<byte> target) => _block.TryWrite(target);
        public void Freeze() { }
        public bool Field01
        {
            get { return _block.A.A.A.A.A.A.BoolValue; }
            set { _block.A.A.A.A.A.A.BoolValue = value; }
        }

        public double Field02LE
        {
            get { return _block.A.B.A.DoubleValueLE; }
            set { _block.A.B.A.DoubleValueLE = value; }
        }

        public double Field02BE
        {
            get { return _block.A.B.B.DoubleValueBE; }
            set { _block.A.B.B.DoubleValueBE = value; }
        }

        public Guid Field03
        {
            get { return _block.B.A.GuidValueLE; }
            set { _block.B.A.GuidValueLE = value; }
        }

    }

    [MemoryPackable]
    public sealed partial class MemoryPackMyDTO : IMyDTO
    {
        public void Freeze() { }

        [MemoryPackInclude] public bool Field01 { get; set; }
        [MemoryPackInclude] public double Field02LE { get; set; }
        [MemoryPackInclude] public double Field02BE { get; set; }
        [MemoryPackInclude] public Guid Field03 { get; set; }
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
    //[SimpleJob(RuntimeMoniker.Net80)]
    [SimpleJob(RuntimeMoniker.Net90)]
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

        private MemoryPackMyDTO MakeMyDTO_MemoryPack(ValueKind id)
        {
            var dto = new MemoryPackMyDTO();
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
                case ValueKind.Guid:
                    dto.Field03 = guidValue;
                    break;
                default:
                    break;
            }
            return dto;
        }

        private NetStruxMyDTO MakeMyDTO_NetStrux(ValueKind id)
        {
            var dto = new NetStruxMyDTO();
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
        public int Roundtrip_MemoryPack()
        {
            var dto = MakeMyDTO_MemoryPack(Kind);
            dto.Freeze();
            ReadOnlyMemory<byte> buffer = MemoryPackSerializer.Serialize<MemoryPackMyDTO>(dto);
            var copy = MemoryPackSerializer.Deserialize<MemoryPackMyDTO>(buffer.Span);
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

        [Benchmark]
        public int Roundtrip_NetStrux()
        {
            var dto = MakeMyDTO_NetStrux(Kind);
            dto.Freeze();
            Span<byte> buffer = stackalloc byte[64];
            dto.TryWrite(buffer);
            var copy = new NetStruxMyDTO();
            copy.TryRead(buffer);
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
