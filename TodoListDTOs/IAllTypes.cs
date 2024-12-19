using DTOMaker.Models;
using DataFac.Runtime;
using System;
using System.Buffers.Binary;
using System.Diagnostics;
using System.Text;
using DTOMaker.Models.MessagePack;
using DTOMaker.Models.MemBlocks;

[assembly: Domain]

namespace TodoListDTOs
{
    [Entity]
    [EntityTag(1)]
    [EntityLayout(LayoutMethod.Explicit, 128)]
    public interface IAllTypesExplicit
    {
        [Member(1)][Offset(0)] bool Field01 { get; }
        [Member(2)][Offset(1)] sbyte Field02 { get; }
        [Member(3)][Offset(2)] byte Field03 { get; }
        [Member(4)][Offset(4)] short Field04 { get; }
        [Member(5)][Offset(6)] ushort Field05 { get; }
        [Member(6)][Offset(8)] char Field06 { get; }
        [Member(7)][Offset(12)] int Field07 { get; }
        [Member(8)][Offset(16)] uint Field08 { get; }
        [Member(9)][Offset(20)] float Field09 { get; }
        [Member(10)][Offset(24)] long Field10 { get; }
        [Member(11)][Offset(32)] ulong Field11 { get; }
        [Member(12)][Offset(40)] double Field12 { get; }
        [Member(13)][Offset(48)] Guid Field13 { get; }
        [Member(14)][Offset(64)] Decimal Field14 { get; }
        [Member(15)][Offset(80)] int Field15_Data { get; }
        //DayOfWeek Field15 { get; }
    }

    internal static class BufferHelpers
    {
        public static char CharReverser(char ch) => (char)BinaryPrimitives.ReverseEndianness(ch);
        public static ReadOnlyMemory<T> ReverseEndianness<T>(this ReadOnlySpan<T> source, Func<T, T> reverserFn)
        {
            var result = new T[source.Length];
            for (var i = 0; i < source.Length; i++)
            {
                result[i] = reverserFn(source[i]);
            }
            return result;
        }

        public static ReadOnlyMemory<T> CorrectEndianness<T>(this ReadOnlyMemory<T> source, bool isBigEndian, Func<T, T> reverserFn)
        {
            return BitConverter.IsLittleEndian != isBigEndian
                ? source
                : source.Span.ReverseEndianness(reverserFn);
        }
    }

    [Entity]
    [EntityTag(2)]
    [EntityLayout(LayoutMethod.SequentialV1)]
    public interface IAllTypesSequential
    {
        [Member(1)] bool Field01 { get; }
        [Member(2)] sbyte Field02 { get; }
        [Member(3)] byte Field03 { get; }
        [Member(4)] short Field04 { get; }
        [Member(5)] ushort Field05 { get; }
        [Member(6)] char Field06 { get; }
        [Member(7)] int Field07 { get; }
        [Member(8)] uint Field08 { get; }
        [Member(9)] float Field09 { get; }
        [Member(10)] long Field10 { get; }
        [Member(11)] ulong Field11 { get; }
        [Member(12)] double Field12 { get; }
        [Member(13)] Guid Field13 { get; }
        [Member(14)] Decimal Field14 { get; }
        [Member(15)]int Field15_Data { get; }
        //DayOfWeek Field15 { get; }
        [Member(16)][Length(32)] ReadOnlyMemory<byte> Field16_Buffer { get; set; }
        [Member(17)][Length(32)] ReadOnlyMemory<char> Field18_Buffer { get; set; }
        [Member(18)] int Field16_Length { get; set; }
        [Member(19)] int Field18_Length { get; set; }
        //ReadOnlyMemory<byte>? Field16_Binary { get; set; }
        //{
        //    get
        //    {
        //        int length = this.Field16_Length;
        //        return length switch
        //        {
        //            < 0 => null,
        //            0 => ReadOnlyMemory<byte>.Empty,
        //            _ => Field16_Buffer.Slice(0, length),
        //        };
        //    }
        //}
        //Octets? Field16_Octets { get; set; }
        //{
        //    get
        //    {
        //        int length = this.Field16_Length;
        //        return length switch
        //        {
        //            < 0 => null,
        //            0 => Octets.Empty,
        //            _ => Octets.UnsafeWrap(Field16_Buffer.Slice(0, length)),
        //        };
        //    }
        //}
        string? Field16_StringUTF8 { get; set; }
        //{
        //    get
        //    {
        //        int length = this.Field16_Length;
        //        return length switch
        //        {
        //            < 0 => null,
        //            0 => string.Empty,
        //            _ => Encoding.UTF8.GetString(Field16_Buffer.Slice(0, length).Span),
        //        };
        //    }
        //    set
        //    {
        //        if (value is null)
        //        {
        //            Field16_Buffer = ReadOnlyMemory<byte>.Empty;
        //            Field16_Length = -1;
        //        }
        //        else if (value.Length == 0)
        //        {
        //            Field16_Buffer = ReadOnlyMemory<byte>.Empty;
        //            Field16_Length = 0;
        //        }
        //        else
        //        {
        //            ReadOnlyMemory<byte> encoded = Encoding.UTF8.GetBytes(value);
        //            Field16_Buffer = encoded;
        //            Field16_Length = encoded.Length;
        //        }
        //    }
        //}
        //ReadOnlyMemory<char>? Field18_Binary { get; set; }
        //{
        //    get
        //    {
        //        const bool _isBigEndian = false;
        //        int length = this.Field18_Length;
        //        return length switch
        //        {
        //            < 0 => null,
        //            0 => ReadOnlyMemory<char>.Empty,
        //            _ => Field18_Buffer.Slice(0, length).CorrectEndianness(_isBigEndian, BufferHelpers.CharReverser),
        //        };
        //    }
        //}
        //string? Field18_String { get; set; }
        //{
        //    get
        //    {
        //        const bool _isBigEndian = false;
        //        int length = this.Field18_Length;
        //        return length switch
        //        {
        //            < 0 => null,
        //            0 => string.Empty,
        //            _ => new string(Field18_Buffer.Slice(0, length).CorrectEndianness(_isBigEndian, BufferHelpers.CharReverser).Span),
        //        };
        //    }
        //}
        //[Member(20)] Int128 Field20 { get; }
        //[Member(21)] UInt128 Field21 { get; }

        //int? OptionalField07 { get; }
    }
}
