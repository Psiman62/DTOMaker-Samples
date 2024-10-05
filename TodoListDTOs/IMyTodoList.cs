using DTOMaker.Models;
using MessagePack;
using System.Runtime.Serialization;

[assembly: Domain]

namespace TodoListDTOs
{
    [Entity]
    [EntityLayout(LayoutMethod.Explicit, 64)]
    public interface IAllTypesExplicit
    {
        [Member(1)][MemberLayout(0,1)] bool Field01 { get; }

        [Member(2)][MemberLayout(1, 1)] sbyte Field02 { get; }
        [Member(3)][MemberLayout(2, 1)] byte Field03 { get; }
        [Member(4)][MemberLayout(4, 2)] short Field04 { get; }
        [Member(5)][MemberLayout(6, 2)] ushort Field05 { get; }
        //[Member(6)] char Field06 { get; }
        //[Member(7)] Half Field07 { get; }
        [Member(6)][MemberLayout(8, 4)] int Field08 { get; }
        [Member(7)][MemberLayout(12, 4)] uint Field09 { get; }
        [Member(8)][MemberLayout(16, 4)] float Field10 { get; }

        int? OptionalField08 { get; }
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
        //[Member(6)] char Field06 { get; }
        //[Member(7)] Half Field07 { get; }
        [Member(6)] int Field08 { get; }
        [Member(7)] uint Field09 { get; }
        [Member(8)] float Field10 { get; }

        int? OptionalField08 { get; }
    }
}
