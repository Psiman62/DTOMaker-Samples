using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using DTOMaker.Models;
using DTOMaker.Runtime;
using MemoryPack;
using MessagePack;
using System;
using System.Buffers.Binary;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

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

    [StructLayout(LayoutKind.Explicit, Size = 1)]
    public struct BlockB001
    {
        [FieldOffset(0)] public bool BoolValue;
        [FieldOffset(0)] public sbyte SByteValue;
        [FieldOffset(0)] public byte ByteValue;

        public bool TryRead(ReadOnlySpan<byte> source) => MemoryMarshal.TryRead(source.Slice(0, 1), out this);
        public bool TryWrite(Span<byte> target) => MemoryMarshal.TryWrite(target.Slice(0, 1), ref this);
    }

    [StructLayout(LayoutKind.Explicit, Size = 2)]
    public struct BlockB002
    {
        [FieldOffset(0)] public BlockB001 A;
        [FieldOffset(1)] public BlockB001 B;

        public bool TryRead(ReadOnlySpan<byte> source) => MemoryMarshal.TryRead(source.Slice(0, 2), out this);
        public bool TryWrite(Span<byte> target) => MemoryMarshal.TryWrite(target.Slice(0, 2), ref this);
    }

    [StructLayout(LayoutKind.Explicit, Size = 4)]
    public struct BlockB004
    {
        [FieldOffset(0)] public BlockB002 A;
        [FieldOffset(2)] public BlockB002 B;

        public bool TryRead(ReadOnlySpan<byte> source) => MemoryMarshal.TryRead(source.Slice(0, 4), out this);
        public bool TryWrite(Span<byte> target) => MemoryMarshal.TryWrite(target.Slice(0, 4), ref this);
    }

    [StructLayout(LayoutKind.Explicit, Size = 8)]
    public struct BlockB008
    {
        [FieldOffset(0)] public BlockB004 A;
        [FieldOffset(4)] public BlockB004 B;

        public bool TryRead(ReadOnlySpan<byte> source) => MemoryMarshal.TryRead(source.Slice(0, 8), out this);
        public bool TryWrite(Span<byte> target) => MemoryMarshal.TryWrite(target.Slice(0, 8), ref this);

        [FieldOffset(0)] public long _long;
        [FieldOffset(0)] public double _double;
        public double DoubleValueLE
        {
            get
            {
                if (BitConverter.IsLittleEndian)
                    return _double;
                else
                    return BitConverter.Int64BitsToDouble(BinaryPrimitives.ReverseEndianness(_long));
            }
            set
            {
                if (BitConverter.IsLittleEndian)
                    _double = value;
                else
                    _long = BinaryPrimitives.ReverseEndianness(BitConverter.DoubleToInt64Bits(value));
            }
        }
        public double DoubleValueBE
        {
            get
            {
                if (BitConverter.IsLittleEndian)
                    return BitConverter.Int64BitsToDouble(BinaryPrimitives.ReverseEndianness(_long));
                else
                    return _double;
            }
            set
            {
                if (BitConverter.IsLittleEndian)
                    _long = BinaryPrimitives.ReverseEndianness(BitConverter.DoubleToInt64Bits(value));
                else
                    _double = value;
            }
        }
    }

    [StructLayout(LayoutKind.Explicit, Size = 16)]
    public struct BlockB016
    {
        [FieldOffset(0)] public BlockB008 A;
        [FieldOffset(8)] public BlockB008 B;

        public bool TryRead(ReadOnlySpan<byte> source) => MemoryMarshal.TryRead(source.Slice(0, 16), out this);
        public bool TryWrite(Span<byte> target) => MemoryMarshal.TryWrite(target.Slice(0, 16), ref this);

        [FieldOffset(0)] public Guid _guid;
        public Guid GuidValueLE
        {
            get
            {
                if (BitConverter.IsLittleEndian)
                    return _guid;
                else
                    throw new NotImplementedException();
            }
            set
            {
                if (BitConverter.IsLittleEndian)
                    _guid = value;
                else
                    throw new NotImplementedException();
            }
        }
    }

    [StructLayout(LayoutKind.Explicit, Size = 32)]
    public struct BlockB032
    {
        [FieldOffset(0)] public BlockB016 A;
        [FieldOffset(16)] public BlockB016 B;

        public bool TryRead(ReadOnlySpan<byte> source) => MemoryMarshal.TryRead(source.Slice(0, 32), out this);
        public bool TryWrite(Span<byte> target) => MemoryMarshal.TryWrite(target.Slice(0, 32), ref this);
    }

    [StructLayout(LayoutKind.Explicit, Size = 64)]
    public struct BlockB064
    {
        [FieldOffset(0)] public BlockB032 A;
        [FieldOffset(32)] public BlockB032 B;

        public bool TryRead(ReadOnlySpan<byte> source) => MemoryMarshal.TryRead(source.Slice(0, 64), out this);
        public bool TryWrite(Span<byte> target) => MemoryMarshal.TryWrite(target.Slice(0, 64), ref this);
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
