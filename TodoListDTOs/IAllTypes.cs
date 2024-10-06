using DTOMaker.Models;
using System;

[assembly: Domain]

namespace TodoListDTOs
{
    [Entity]
    [EntityLayout(LayoutMethod.Explicit, 128)]
    public interface IAllTypesExplicit
    {
        [Member(1)][MemberLayout(0)] bool Field01 { get; }
        [Member(2)][MemberLayout(1)] sbyte Field02 { get; }
        [Member(3)][MemberLayout(2)] byte Field03 { get; }
        [Member(4)][MemberLayout(4)] short Field04 { get; }
        [Member(5)][MemberLayout(6)] ushort Field05 { get; }
        [Member(6)][MemberLayout(8)] char Field06 { get; }
        [Member(7)][MemberLayout(12)] int Field07 { get; }
        [Member(8)][MemberLayout(16)] uint Field08 { get; }
        [Member(9)][MemberLayout(20)] float Field09 { get; }
        [Member(10)][MemberLayout(24)] long Field10 { get; }
        [Member(11)][MemberLayout(32)] ulong Field11 { get; }
        [Member(12)][MemberLayout(40)] double Field12 { get; }
        [Member(13)][MemberLayout(48)] Guid Field13 { get; }
        [Member(14)][MemberLayout(64)] Decimal Field14 { get; }

        int? OptionalField07 { get; }
    }

    [Entity]
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
#if NET8_0_OR_GREATER
        [Member(15)] Int128 Field15 { get; }
        [Member(16)] UInt128 Field16 { get; }
#endif

        int? OptionalField07 { get; }
    }
}
