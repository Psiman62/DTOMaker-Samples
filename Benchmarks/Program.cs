﻿using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using DTOMaker.Models;
using DTOMaker.Runtime;
using MessagePack;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Benchmarks
{
    [Entity]
    [EntityLayout(LayoutMethod.SequentialV1)]
    public interface IMyDTO
    {
        [Member(1)] bool Field01 { get; }
        [Member(2)] long Field02 { get; }
        [Member(3)] Guid Field03 { get; }
    }

    public enum ValueKind
    {
        Bool,
        Int64,
        Guid,
        // todo strings
    }

    [SimpleJob(RuntimeMoniker.Net481)]
    [SimpleJob(RuntimeMoniker.Net80)]
    [MemoryDiagnoser]
    public class DTORoundtrips
    {
        [Params(ValueKind.Bool, ValueKind.Int64, ValueKind.Guid)]
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
                case ValueKind.Int64:
                    dto.Field02 = long.MaxValue;
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
                case ValueKind.Int64:
                    dto.Field02 = long.MaxValue;
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
