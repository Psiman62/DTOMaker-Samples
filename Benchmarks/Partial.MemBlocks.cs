using System;
using System.Text;

namespace Benchmarks.MemBlocks
{
    public partial class StringsDTO
    {
        public string? Field05
        {
            get
            {
                return Field05_HasValue ? Field05_Value : null;
            }
            set
            {
                if (value is null)
                {
                    Field05_HasValue = false;
                    Field05_Value = "";
                }
                else
                {
                    Field05_HasValue = true;
                    Field05_Value = value;
                }
            }
        }
    }
    public partial class MyDTO
    {
        public string? Field05
        {
            get
            {
                short length = this.Field05_Length;
                if (length < 0) return null;
                else if (length == 0) return string.Empty;
                else
                {
                    return Encoding.UTF8.GetString(this.Field05_Data.Span.Slice(0, length));
                }
            }
            set
            {
                if (value is null)
                {
                    this.Field05_Length = -1;
                }
                else if (value.Length == 0)
                {
                    this.Field05_Length = 0;
                }
                else
                {
                    var buffer = Encoding.UTF8.GetBytes(value);
                    this.Field05_Data = buffer;
                    this.Field05_Length = (short)buffer.Length;
                }
            }
        }
    }
}
