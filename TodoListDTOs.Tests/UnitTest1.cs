using FluentAssertions;

namespace TodoListDTOs.Tests
{
    [TestClass]
    public class DTORegressionTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            var orig = new TodoListDTOs.MyTodoList();
            orig.Id = 123;
            ReadOnlyMemory<byte> buffer = orig.Block;

            var copy = new MyTodoList(buffer.Span);
            copy.Id.Should().Be(orig.Id);
            //todo copy.Equals(orig).Should().BeTrue();
        }
    }
}