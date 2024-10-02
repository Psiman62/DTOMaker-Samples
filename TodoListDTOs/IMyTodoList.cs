using DTOMaker.Models;

[assembly: Domain]

namespace TodoListDTOs
{
    [Entity]
    [EntityLayout(LayoutMethod.Explicit, 128)]
    public interface IMyTodoList
    {
        [Member(1)]
        [MemberLayout(0, 8)]
        long Id { get; }

        [Member(2)]
        [MemberLayout(8, 4)]
        int Code { get; }

        [Member(3)]
        [MemberLayout(12, 1)]
        bool HasValue { get; }

        [Member(4)]
        [MemberLayout(16, 8)]
        double Value { get; }

        [Member(5)]
        [MemberLayout(24, 4)]
        uint Value2 { get; }

        [Member(6)]
        [MemberLayout(28, 1)]
        byte Value3 { get; }

        [Member(7)]
        [MemberLayout(29, 1)]
        sbyte Value4 { get; }

        // todo
        //[Member(6)]
        //[MemberLayout(24, 4)]
        //float Value2 { get; }

    }
}
