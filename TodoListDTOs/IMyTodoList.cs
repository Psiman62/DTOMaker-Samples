using DTOMaker.Models;

[assembly: Domain]

namespace TodoListDTOs
{
    [Entity]
    [EntityLayout(128)]
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
    }
}
