using MessagePack;
using System;
using System.Text;

namespace TodoListDTOs.MessagePack
{
    public partial class AllTypesSequential : IAllTypesSequential
    {
        public string? Field16_StringUTF8
        {
            get
            {
                int length = this.Field16_Length;
                return length switch
                {
                    < 0 => null,
                    0 => string.Empty,
#if NET6_0_OR_GREATER
                    _ => Encoding.UTF8.GetString(this.Field16_Buffer.Slice(0, length).Span),
#else
                    _ => Encoding.UTF8.GetString(this.Field16_Buffer.Slice(0, length).ToArray()),
#endif
                };
            }
            set
            {
                if (value is null)
                {
                    Field16_Buffer = ReadOnlyMemory<byte>.Empty;
                    Field16_Length = -1;
                }
                else if (value.Length == 0)
                {
                    Field16_Buffer = ReadOnlyMemory<byte>.Empty;
                    Field16_Length = 0;
                }
                else
                {
                    ReadOnlyMemory<byte> encoded = Encoding.UTF8.GetBytes(value);
                    Field16_Buffer = encoded;
                    Field16_Length = encoded.Length;
                }
            }
        }
    }
}

namespace TodoListDTOs.CSPoco
{
    public partial class AllTypesSequential : IAllTypesSequential
    {
        public string? Field16_StringUTF8
        {
            get
            {
                int length = this.Field16_Length;
                return length switch
                {
                    < 0 => null,
                    0 => string.Empty,
#if NET6_0_OR_GREATER
                    _ => Encoding.UTF8.GetString(this.Field16_Buffer.Slice(0, length).Span),
#else
                    _ => Encoding.UTF8.GetString(this.Field16_Buffer.Slice(0, length).ToArray()),
#endif
                };
            }
            set
            {
                if (value is null)
                {
                    Field16_Buffer = ReadOnlyMemory<byte>.Empty;
                    Field16_Length = -1;
                }
                else if (value.Length == 0)
                {
                    Field16_Buffer = ReadOnlyMemory<byte>.Empty;
                    Field16_Length = 0;
                }
                else
                {
                    ReadOnlyMemory<byte> encoded = Encoding.UTF8.GetBytes(value);
                    Field16_Buffer = encoded;
                    Field16_Length = encoded.Length;
                }
            }
        }
    }
}
