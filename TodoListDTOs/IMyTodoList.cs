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
    }
}
