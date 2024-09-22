using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoListDTOs
{
    public partial class MyTodoList
    {
        public MyTodoList(int notUsed) => _block = new byte[BlockSize];
        public MyTodoList(ReadOnlySpan<byte> source, int notUsed) => _block = source.Slice(0, BlockSize).ToArray(); // source.Slice(0, BlockSize).CopyTo(_block.Span);
    }
}
