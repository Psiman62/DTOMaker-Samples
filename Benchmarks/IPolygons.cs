using DTOMaker.Models;
using DTOMaker.Models.MemBlocks;
using DTOMaker.Models.MessagePack;

namespace Benchmarks
{
    [Entity]
    [EntityTag(3)]
    [EntityLayout("Polygon", LayoutMethod.SequentialV1)]
    public interface IPolygon { }

    [Entity]
    [EntityTag(4)]
    [EntityLayout("Triangle", LayoutMethod.SequentialV1)]
    public interface ITriangle : IPolygon { }

    [Entity]
    [EntityTag(5)]
    [EntityLayout("Equilateral", LayoutMethod.SequentialV1)]
    public interface IEquilateral : ITriangle
    {
        [Member(1)] double Length { get; set; }
    }

    [Entity]
    [EntityTag(6)]
    [EntityLayout("RightTriangle", LayoutMethod.SequentialV1)]
    public interface IRightTriangle : ITriangle
    {
        [Member(1)] double Length { get; set; }
        [Member(2)] double Height { get; set; }
    }

    [Entity]
    [EntityTag(7)]
    [EntityLayout("Quadrilateral", LayoutMethod.SequentialV1)]
    public interface IQuadrilateral : IPolygon { }

    [Entity]
    [EntityTag(8)]
    [EntityLayout("Square", LayoutMethod.SequentialV1)]
    public interface ISquare : IQuadrilateral
    {
        [Member(1)] double Length { get; set; }
    }

    [Entity]
    [EntityTag(9)]
    [EntityLayout("Rectangle", LayoutMethod.SequentialV1)]
    public interface IRectangle : IQuadrilateral
    {
        [Member(1)] double Length { get; set; }
        [Member(2)] double Height { get; set; }
    }

}
