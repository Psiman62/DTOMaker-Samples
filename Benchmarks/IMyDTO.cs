using DTOMaker.Models;
using System;

namespace Benchmarks
{
    [Entity(1)]
    [EntityLayout(LayoutMethod.SequentialV1)]
    public interface IMyDTO
    {
        [Member(1)] bool Field01 { get; set; }
        [Member(2)][MemberLayout(0, false)] double Field02LE { get; set; }
        [Member(3)][MemberLayout(0, true)] double Field02BE { get; set; }
        [Member(4)] Guid Field03 { get; set; }
        [Member(5)] short Field05_Length { get; set; }
        [Member(6)][MemberLayout(arrayLength: 128)] ReadOnlyMemory<byte> Field05_Data { get; set; }
        string? Field05 { get; set; }
    }
}
