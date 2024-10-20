using DTOMaker.Models;
using System;

[assembly: Domain]

namespace TodoListDTOs
{
    [Entity]
    [EntityLayout(LayoutMethod.Explicit, 256)]
    public interface IAllTypesExplicit
    {
        [Member(1)][MemberLayout(0, 1)] bool Field01 { get; }
        [Member(2)][MemberLayout(2, 3)] sbyte Field02 { get; }
        [Member(3)][MemberLayout(4, 5)] byte Field03 { get; }
        [Member(4)][MemberLayout(6, 8)] short Field04 { get; }
        [Member(5)][MemberLayout(10, 12)] ushort Field05 { get; }
        [Member(6)][MemberLayout(14, 16)] char Field06 { get; }
        [Member(7)][MemberLayout(20, 24)] int Field07 { get; }
        [Member(8)][MemberLayout(28, 32)] uint Field08 { get; }
        [Member(9)][MemberLayout(36, 40)] float Field09 { get; }
        [Member(10)][MemberLayout(48, 56)] long Field10 { get; }
        [Member(11)][MemberLayout(64, 72)] ulong Field11 { get; }
        [Member(12)][MemberLayout(80, 88)] double Field12 { get; }
        [Member(13)][MemberLayout(96, 112)] Guid Field13 { get; }
        [Member(14)][MemberLayout(128, 144)] Decimal Field14 { get; }
        [Member(15)][MemberLayout(148, 152)] DayOfWeek Field15 { get; }

        //int? OptionalField07 { get; }
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
        [Obsolete][Member(15)] DayOfWeek Field15 { get; }
#if NET8_0_OR_GREATER
        [Member(16)] Int128 Field16 { get; }
        [Member(17)] UInt128 Field17 { get; }
#endif

        //int? OptionalField07 { get; }
    }
}
