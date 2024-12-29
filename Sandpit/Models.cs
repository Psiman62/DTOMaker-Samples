using DTOMaker.Models;
using DTOMaker.Models.MessagePack;

namespace Sandpit
{
    [Entity]
    [EntityKey(3)]
    public interface IPolygon { }

    [Entity]
    [EntityKey(4)]
    public interface ITriangle : IPolygon { }

    [Entity]
    [EntityKey(5)]
    public interface IEquilateral : ITriangle
    {
        [Member(1)] double Length { get; set; }
    }

    [Entity]
    [EntityKey(6)]
    public interface IRightTriangle : ITriangle
    {
        [Member(1)] double Length { get; set; }
        [Member(2)] double Height { get; set; }
    }

    [Entity]
    [EntityKey(7)]
    public interface IQuadrilateral : IPolygon { }

    [Entity]
    [EntityKey(8)]
    public interface ISquare : IQuadrilateral
    {
        [Member(1)] double Length { get; set; }
    }

    [Entity]
    [EntityKey(9)]
    public interface IRectangle : IQuadrilateral
    {
        [Member(1)] double Length { get; set; }
        [Member(2)] double Height { get; set; }
    }

}
