using DTOMaker.Models;
using DTOMaker.Models.MemBlocks;
using DTOMaker.Models.MessagePack;
using System;

namespace SampleDTO.Shapes
{
    [Entity]
    [EntityKey(3)]
    [Id("Polygon")]
    [Layout(LayoutMethod.SequentialV1)]
    public interface IShape { }

    [Entity]
    [EntityKey(4)]
    [Id("Triangle")]
    [Layout(LayoutMethod.SequentialV1)]
    public interface ITriangle : IShape { }

    [Entity]
    [EntityKey(5)]
    [Id("Equilateral")]
    [Layout(LayoutMethod.SequentialV1)]
    public interface IEquilateral : ITriangle
    {
        [Member(1)] double Length { get; set; }
    }

    [Entity]
    [EntityKey(6)]
    [Id("RightTriangle")]
    [Layout(LayoutMethod.SequentialV1)]
    public interface IRightTriangle : ITriangle
    {
        [Member(1)] double Length { get; set; }
        [Member(2)] double Height { get; set; }
    }

    [Entity]
    [EntityKey(7)]
    [Id("Quadrilateral")]
    [Layout(LayoutMethod.SequentialV1)]
    public interface IQuadrilateral : IShape { }

    [Entity]
    [EntityKey(8)]
    [Id("Square")]
    [Layout(LayoutMethod.SequentialV1)]
    public interface ISquare : IQuadrilateral
    {
        [Member(1)] double Length { get; set; }
    }

    [Entity]
    [EntityKey(9)]
    [Id("Rectangle")]
    [Layout(LayoutMethod.SequentialV1)]
    public interface IRectangle : IQuadrilateral
    {
        [Member(1)] double Length { get; set; }
        [Member(2)] double Height { get; set; }
    }

}
