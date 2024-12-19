using MemoryPack;
using System;

namespace Benchmarks
{
    [MemoryPackable]
    public sealed partial class MemoryPackMyDTO : IMyDTO
    {
        public void Freeze() { }

        [MemoryPackInclude] public bool Field01 { get; set; }
        [MemoryPackInclude] public double Field02LE { get; set; }
        [MemoryPackInclude] public double Field02BE { get; set; }
        [MemoryPackInclude] public Guid Field03 { get; set; }
        [MemoryPackIgnore] public short Field05_Length { get; set; }
        [MemoryPackIgnore] public ReadOnlyMemory<byte> Field05_Data { get; set; }
        [MemoryPackInclude] public string? Field05 { get; set; }
    }

    [MemoryPackable]
    public sealed partial class MemoryPackStringsDTO : IStringsDTO
    {
        public void Freeze() { }

        [MemoryPackInclude] public string Field05_Value { get; set; } = "";
        [MemoryPackInclude] public bool Field05_HasValue { get; set; }

        [MemoryPackIgnore]
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
}
