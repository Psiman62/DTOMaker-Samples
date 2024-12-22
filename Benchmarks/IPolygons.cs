using DTOMaker.Models;
using DTOMaker.Models.MemBlocks;
using DTOMaker.Models.MessagePack;

namespace Benchmarks
{
    [Entity]
    [EntityTag(3)]
    [Id("Polygon")][Layout(LayoutMethod.SequentialV1)]
    public interface IPolygon { }

    [Entity]
    [EntityTag(4)]
    [Id("Triangle")][Layout(LayoutMethod.SequentialV1)]
    public interface ITriangle : IPolygon { }

    [Entity]
    [EntityTag(5)]
    [Id("Equilateral")][Layout(LayoutMethod.SequentialV1)]
    public interface IEquilateral : ITriangle
    {
        [Member(1)] double Length { get; set; }
    }

    [Entity]
    [EntityTag(6)]
    [Id("RightTriangle")][Layout(LayoutMethod.SequentialV1)]
    public interface IRightTriangle : ITriangle
    {
        [Member(1)] double Length { get; set; }
        [Member(2)] double Height { get; set; }
    }

    [Entity]
    [EntityTag(7)]
    [Id("Quadrilateral")][Layout(LayoutMethod.SequentialV1)]
    public interface IQuadrilateral : IPolygon { }

    [Entity]
    [EntityTag(8)]
    [Id("Square")][Layout(LayoutMethod.SequentialV1)]
    public interface ISquare : IQuadrilateral
    {
        [Member(1)] double Length { get; set; }
    }

    [Entity]
    [EntityTag(9)]
    [Id("Rectangle")][Layout(LayoutMethod.SequentialV1)]
    public interface IRectangle : IQuadrilateral
    {
        [Member(1)] double Length { get; set; }
        [Member(2)] double Height { get; set; }
    }

}
