using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Order;
using MemoryPack;
using MessagePack;
using System;

namespace Benchmarks
{
    [SimpleJob(RuntimeMoniker.Net90)]
    [MemoryDiagnoser]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    public class DTORoundtripStrings
    {
        [Params(ValueKind.StringNull, ValueKind.StringFull)]
        public ValueKind Kind;

        private static readonly string StringWith255Chars =
            "0123456789abcdef" +
            "0123456789abcdef" +
            "0123456789abcdef" +
            "0123456789abcdef" +
            "0123456789abcdef" +
            "0123456789abcdef" +
            "0123456789abcdef" +
            "0123456789abcdef" +
            "0123456789abcdef" +
            "0123456789abcdef" +
            "0123456789abcdef" +
            "0123456789abcdef" +
            "0123456789abcdef" +
            "0123456789abcdef" +
            "0123456789abcdef" +
            "0123456789abcde";

        private MessagePack.StringsDTO MakeMyDTO_MessagePack(ValueKind kind)
        {
            var dto = new MessagePack.StringsDTO();
            switch (Kind)
            {
                case ValueKind.StringNull:
                    dto.Field05 = null;
                    break;
                case ValueKind.StringZero:
                    dto.Field05 = string.Empty;
                    break;
                case ValueKind.StringFull:
                    dto.Field05 = StringWith255Chars;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(kind), kind, null);
            }
            return dto;
        }

        [Benchmark(Baseline = true)]
        public int Roundtrip_MessagePack()
        {
            var dto = MakeMyDTO_MessagePack(Kind);
            dto.Freeze();
            var buffer = MessagePackSerializer.Serialize<MessagePack.StringsDTO>(dto);
            var copy = MessagePackSerializer.Deserialize<MessagePack.StringsDTO>(buffer, out int bytesRead);
            dto.Freeze();
            return 0;
        }

        private MemBlocks.StringsDTO MakeMyDTO_MemBlocks(ValueKind kind)
        {
            var dto = new MemBlocks.StringsDTO();
            switch (Kind)
            {
                case ValueKind.StringNull:
                    dto.Field05 = null;
                    break;
                case ValueKind.StringZero:
                    dto.Field05 = string.Empty;
                    break;
                case ValueKind.StringFull:
                    dto.Field05 = StringWith255Chars;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(kind), kind, null);
            }
            return dto;
        }

        [Benchmark]
        public int Roundtrip_MemBlocks()
        {
            var dto = MakeMyDTO_MemBlocks(Kind);
            dto.Freeze();
            var buffers = dto.GetBuffers();
            var copy = new MemBlocks.StringsDTO(buffers);
            dto.Freeze();
            return 0;
        }

        private NetStruxStringsDTO MakeMyDTO_NetStrux(ValueKind kind)
        {
            var dto = new NetStruxStringsDTO();
            switch (Kind)
            {
                case ValueKind.StringNull:
                    dto.Field05 = null;
                    break;
                case ValueKind.StringZero:
                    dto.Field05 = string.Empty;
                    break;
                case ValueKind.StringFull:
                    dto.Field05 = StringWith255Chars;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(kind), kind, null);
            }
            return dto;
        }

        [Benchmark]
        public int Roundtrip_NetStrux()
        {
            var dto = MakeMyDTO_NetStrux(Kind);
            dto.Freeze();
            Span<byte> buffer = stackalloc byte[512];
            dto.TryWrite(buffer);
            var copy = new NetStruxMyDTO();
            copy.TryRead(buffer);
            return 0;
        }

        private MemoryPackStringsDTO MakeStringsDTO_MemoryPack(ValueKind id)
        {
            var dto = new MemoryPackStringsDTO();
            switch (Kind)
            {
                case ValueKind.StringNull:
                    dto.Field05 = null;
                    break;
                case ValueKind.StringZero:
                    dto.Field05 = string.Empty;
                    break;
                case ValueKind.StringFull:
                    dto.Field05 = StringWith255Chars;
                    break;
                default:
                    break;
            }
            return dto;
        }

        [Benchmark]
        public int Roundtrip_MemoryPack()
        {
            var dto = MakeStringsDTO_MemoryPack(Kind);
            dto.Freeze();
            ReadOnlyMemory<byte> buffer = MemoryPackSerializer.Serialize<MemoryPackStringsDTO>(dto);
            var copy = MemoryPackSerializer.Deserialize<MemoryPackStringsDTO>(buffer.Span);
            dto.Freeze();
            return 0;
        }
    }
}
