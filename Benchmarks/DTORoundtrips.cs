using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
#if NET7_0_OR_GREATER
using MemoryPack;
#endif
using MessagePack;
using System;
using System.Linq;

namespace Benchmarks
{
    //[SimpleJob(RuntimeMoniker.Net481)]
    //[SimpleJob(RuntimeMoniker.Net80)]
    [SimpleJob(RuntimeMoniker.Net90)]
    [MemoryDiagnoser]
    public class DTORoundtrips
    {
        //[Params(ValueKind.Bool, ValueKind.DoubleLE, ValueKind.Guid, ValueKind.StringFull)]
        [Params(ValueKind.Bool, ValueKind.Guid, ValueKind.StringFull)]
        public ValueKind Kind;

        private static readonly Guid guidValue = new("cc8af561-5172-43e6-8090-5dc1b2d02e07");

        private static readonly string StringWith128Chars =
            "0123456789abcdef" +
            "0123456789abcdef" +
            "0123456789abcdef" +
            "0123456789abcdef" +
            "0123456789abcdef" +
            "0123456789abcdef" +
            "0123456789abcdef" +
            "0123456789abcdef";

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
                case ValueKind.Guid:
                    dto.Field03 = guidValue;
                    break;
                case ValueKind.StringNull:
                    dto.Field05 = null;
                    break;
                case ValueKind.StringZero:
                    dto.Field05 = string.Empty;
                    break;
                case ValueKind.StringFull:
                    dto.Field05 = StringWith128Chars;
                    break;
                default:
                    break;
            }
            return dto;
        }

#if NET7_0_OR_GREATER
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
                case ValueKind.Guid:
                    dto.Field03 = guidValue;
                    break;
                case ValueKind.StringNull:
                    dto.Field05 = null;
                    break;
                case ValueKind.StringZero:
                    dto.Field05 = string.Empty;
                    break;
                case ValueKind.StringFull:
                    dto.Field05 = StringWith128Chars;
                    break;
                default:
                    break;
            }
            return dto;
        }
#endif

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
                case ValueKind.Guid:
                    dto.Field03 = guidValue;
                    break;
                case ValueKind.StringNull:
                    dto.Field05 = null;
                    break;
                case ValueKind.StringZero:
                    dto.Field05 = string.Empty;
                    break;
                case ValueKind.StringFull:
                    dto.Field05 = StringWith128Chars;
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
                case ValueKind.Guid:
                    dto.Field03 = guidValue;
                    break;
                case ValueKind.StringNull:
                    dto.Field05 = null;
                    break;
                case ValueKind.StringZero:
                    dto.Field05 = string.Empty;
                    break;
                case ValueKind.StringFull:
                    dto.Field05 = StringWith128Chars;
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

#if NET7_0_OR_GREATER
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
#endif

        [Benchmark]
        public int Roundtrip_MemBlocks()
        {
            var dto = MakeMyDTO_MemBlocks(Kind);
            dto.Freeze();
            var buffers = dto.GetBuffers();
            var copy = new MemBlocks.MyDTO(buffers);
            return buffers.Sum(b => b.Length);
        }

        [Benchmark]
        public int Roundtrip_NetStrux()
        {
            var dto = MakeMyDTO_NetStrux(Kind);
            dto.Freeze();
            Span<byte> buffer = stackalloc byte[256];
            dto.TryWrite(buffer);
            var copy = new NetStruxMyDTO();
            copy.TryRead(buffer);
            return buffer.Length;
        }
    }
}
