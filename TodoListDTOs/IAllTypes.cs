using DTOMaker.Models;
using System;

[assembly: Domain]

namespace TodoListDTOs
{
    [Entity]
    [EntityLayout(LayoutMethod.Explicit, 256)]
    public interface IAllTypesExplicit
    {
        [Member(1)][MemberOffset(0)] bool Field01 { get; }
        [Member(2)][MemberOffset(2)] sbyte Field02 { get; }
        [Member(3)][MemberOffset(4)] byte Field03 { get; }
        [Member(4)][MemberOffset(6)] short Field04 { get; }
        [Member(5)][MemberOffset(10)] ushort Field05 { get; }
        [Member(6)][MemberOffset(14)] char Field06 { get; }
        [Member(7)][MemberOffset(20)] int Field07 { get; }
        [Member(8)][MemberOffset(28)] uint Field08 { get; }
        [Member(9)][MemberOffset(36)] float Field09 { get; }
        [Member(10)][MemberOffset(48)] long Field10 { get; }
        [Member(11)][MemberOffset(64)] ulong Field11 { get; }
        [Member(12)][MemberOffset(80)] double Field12 { get; }
        [Member(13)][MemberOffset(96)] Guid Field13 { get; }
        [Member(14)][MemberOffset(128)] Decimal Field14 { get; }
        [Member(15)][MemberOffset(148)] int Field15_Data { get; }
#if NET6_0_OR_GREATER
        DayOfWeek Field15 { get { return (DayOfWeek)Field15_Data; } }
#endif

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
        [Member(15)][MemberOffset(148)] int Field15_Data { get; }
#if NET6_0_OR_GREATER
        DayOfWeek Field15 { get { return (DayOfWeek)Field15_Data; } }
#endif
#if NET8_0_OR_GREATER
        [Member(16)] Int128 Field16 { get; }
        [Member(17)] UInt128 Field17 { get; }
#endif

        //int? OptionalField07 { get; }
    }
}
