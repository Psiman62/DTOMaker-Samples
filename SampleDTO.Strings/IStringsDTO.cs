using DTOMaker.Models;
using DTOMaker.Models.MemBlocks;
using DTOMaker.Models.MessagePack;

namespace SampleDTO.Strings
{
    [Entity]
    [EntityKey(2)]
    [Id("StringsDTO")]
    [Layout(LayoutMethod.SequentialV1)]
    public interface IStringsDTO
    {
        [Member(1)][Length(256)] string Field05_Value { get; set; }
        [Member(2)] bool Field05_HasValue { get; set; }
        string? Field05 { get; set; }
    }

}
