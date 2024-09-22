using DTOMaker.Models;

[assembly: Domain]

namespace TodoListDTOs
{
    [Entity(128)]
    public interface IMyTodoList
    {
        [Member(0, 8)]
        long Id { get; }
    }
}
