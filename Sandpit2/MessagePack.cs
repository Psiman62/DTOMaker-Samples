using DTOMaker.Runtime;
using MessagePack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Sandpit2.MessagePack
{
    [MessagePackObject]
    [Union(Sandpit2.MessagePack.Equilateral.EntityKey, typeof(Sandpit2.MessagePack.Equilateral))]
    [Union(Sandpit2.MessagePack.Rectangle.EntityKey, typeof(Sandpit2.MessagePack.Rectangle))]
    [Union(Sandpit2.MessagePack.RightTriangle.EntityKey, typeof(Sandpit2.MessagePack.RightTriangle))]
    [Union(Sandpit2.MessagePack.Square.EntityKey, typeof(Sandpit2.MessagePack.Square))]
    public abstract class EntityBase : IFreezable, IEquatable<EntityBase>
    {
        public static EntityBase Create(int entityKey, ReadOnlyMemory<byte> buffer)
        {
            return entityKey switch
            {
                Sandpit2.MessagePack.Equilateral.EntityKey => MessagePackSerializer.Deserialize<Sandpit2.MessagePack.Equilateral>(buffer, out _),
                Sandpit2.MessagePack.Rectangle.EntityKey => MessagePackSerializer.Deserialize<Sandpit2.MessagePack.Rectangle>(buffer, out _),
                Sandpit2.MessagePack.RightTriangle.EntityKey => MessagePackSerializer.Deserialize<Sandpit2.MessagePack.RightTriangle>(buffer, out _),
                Sandpit2.MessagePack.Square.EntityKey => MessagePackSerializer.Deserialize<Sandpit2.MessagePack.Square>(buffer, out _),
                _ => throw new ArgumentOutOfRangeException(nameof(entityKey), entityKey, null)
            };
        }

        public EntityBase() { }
        public EntityBase(object? notUsed, bool frozen)
        {
            _frozen = frozen;
        }
        [IgnoreMember]
        private volatile bool _frozen;
        [IgnoreMember]
        public bool IsFrozen => _frozen;
        protected virtual void OnFreeze() { }
        public void Freeze()
        {
            if (_frozen) return;
            _frozen = true;
            OnFreeze();
        }
        protected abstract IFreezable OnPartCopy();
        public IFreezable PartCopy() => OnPartCopy();

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void ThrowIsFrozenException(string? methodName) => throw new InvalidOperationException($"Cannot call {methodName} when frozen.");

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected ref T IfNotFrozen<T>(ref T value, [CallerMemberName] string? methodName = null)
        {
            if (_frozen) ThrowIsFrozenException(methodName);
            return ref value;
        }

        public bool Equals(EntityBase? other) => true;
        public override bool Equals(object? obj) => obj is EntityBase;
        public override int GetHashCode() => 0;
    }
    [MessagePackObject]
    [Union(Equilateral.EntityKey, typeof(Equilateral))]
    [Union(Rectangle.EntityKey, typeof(Rectangle))]
    [Union(RightTriangle.EntityKey, typeof(RightTriangle))]
    [Union(Square.EntityKey, typeof(Square))]
    public abstract partial class Polygon { }
    public partial class Polygon : EntityBase, IPolygon, IEquatable<Polygon>
    {
        // Derived entities: 6
        // - Equilateral
        // - Quadrilateral
        // - Rectangle
        // - RightTriangle
        // - Square
        // - Triangle

        public new const int EntityKey = 3;

        public new static Polygon Create(int entityKey, ReadOnlyMemory<byte> buffer)
        {
            return entityKey switch
            {
                Sandpit2.MessagePack.Equilateral.EntityKey => MessagePackSerializer.Deserialize<Sandpit2.MessagePack.Equilateral>(buffer, out var _),
                Sandpit2.MessagePack.Rectangle.EntityKey => MessagePackSerializer.Deserialize<Sandpit2.MessagePack.Rectangle>(buffer, out var _),
                Sandpit2.MessagePack.RightTriangle.EntityKey => MessagePackSerializer.Deserialize<Sandpit2.MessagePack.RightTriangle>(buffer, out var _),
                Sandpit2.MessagePack.Square.EntityKey => MessagePackSerializer.Deserialize<Sandpit2.MessagePack.Square>(buffer, out var _),
                _ => throw new ArgumentOutOfRangeException(nameof(entityKey), entityKey, null)
            };
        }

        protected override void OnFreeze()
        {
            base.OnFreeze();
            // todo freezable members
        }

        public Polygon() { }
        public Polygon(IPolygon source, bool frozen = false) : base(source, frozen)
        {
        }


        public bool Equals(Polygon? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            if (!base.Equals(other)) return false;
            return true;
        }

        public override bool Equals(object? obj) => obj is Polygon other && Equals(other);
        public static bool operator ==(Polygon? left, Polygon? right) => left is not null ? left.Equals(right) : (right is null);
        public static bool operator !=(Polygon? left, Polygon? right) => left is not null ? !left.Equals(right) : (right is not null);

        private int CalcHashCode()
        {
            HashCode result = new HashCode();
            result.Add(base.GetHashCode());
            return result.ToHashCode();
        }

        [IgnoreMember]
        private int? _hashCode;
        public override int GetHashCode()
        {
            if (_hashCode.HasValue) return _hashCode.Value;
            if (!IsFrozen) return CalcHashCode();
            _hashCode = CalcHashCode();
            return _hashCode.Value;
        }

    }

    [MessagePackObject]
    [Union(Equilateral.EntityKey, typeof(Equilateral))]
    [Union(RightTriangle.EntityKey, typeof(RightTriangle))]
    public abstract partial class Triangle { }
    public partial class Triangle : Polygon, ITriangle, IEquatable<Triangle>
    {
        // Derived entities: 2
        // - Equilateral
        // - RightTriangle

        public new const int EntityKey = 4;

        public new static Triangle Create(int entityKey, ReadOnlyMemory<byte> buffer)
        {
            return entityKey switch
            {
                Sandpit2.MessagePack.Equilateral.EntityKey => MessagePackSerializer.Deserialize<Sandpit2.MessagePack.Equilateral>(buffer, out var _),
                Sandpit2.MessagePack.RightTriangle.EntityKey => MessagePackSerializer.Deserialize<Sandpit2.MessagePack.RightTriangle>(buffer, out var _),
                _ => throw new ArgumentOutOfRangeException(nameof(entityKey), entityKey, null)
            };
        }

        protected override void OnFreeze()
        {
            base.OnFreeze();
            // todo freezable members
        }

        public Triangle() { }
        public Triangle(ITriangle source, bool frozen = false) : base(source, frozen)
        {
        }


        public bool Equals(Triangle? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            if (!base.Equals(other)) return false;
            return true;
        }

        public override bool Equals(object? obj) => obj is Triangle other && Equals(other);
        public static bool operator ==(Triangle? left, Triangle? right) => left is not null ? left.Equals(right) : (right is null);
        public static bool operator !=(Triangle? left, Triangle? right) => left is not null ? !left.Equals(right) : (right is not null);

        private int CalcHashCode()
        {
            HashCode result = new HashCode();
            result.Add(base.GetHashCode());
            return result.ToHashCode();
        }

        [IgnoreMember]
        private int? _hashCode;
        public override int GetHashCode()
        {
            if (_hashCode.HasValue) return _hashCode.Value;
            if (!IsFrozen) return CalcHashCode();
            _hashCode = CalcHashCode();
            return _hashCode.Value;
        }

    }

    [MessagePackObject]
    public partial class Equilateral : Triangle, IEquilateral, IEquatable<Equilateral>
    {
        // Derived entities: 0

        public new const int EntityKey = 5;

        public new static Equilateral Create(int entityKey, ReadOnlyMemory<byte> buffer)
        {
            return entityKey switch
            {
                _ => throw new ArgumentOutOfRangeException(nameof(entityKey), entityKey, null)
            };
        }

        protected override void OnFreeze()
        {
            base.OnFreeze();
            // todo freezable members
        }

        protected override IFreezable OnPartCopy() => new Equilateral(this);

        public Equilateral() { }
        public Equilateral(IEquilateral source, bool frozen = false) : base(source, frozen)
        {
            _Length = source.Length;
        }

        [IgnoreMember]
        private Double _Length = default;
        [Key(201)]
        public Double Length
        {
            get => _Length;
            set => _Length = IfNotFrozen(ref value);
        }


        public bool Equals(Equilateral? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            if (!base.Equals(other)) return false;
            if (_Length != other.Length) return false;
            return true;
        }

        public override bool Equals(object? obj) => obj is Equilateral other && Equals(other);
        public static bool operator ==(Equilateral? left, Equilateral? right) => left is not null ? left.Equals(right) : (right is null);
        public static bool operator !=(Equilateral? left, Equilateral? right) => left is not null ? !left.Equals(right) : (right is not null);

        private int CalcHashCode()
        {
            HashCode result = new HashCode();
            result.Add(base.GetHashCode());
            result.Add(_Length);
            return result.ToHashCode();
        }

        [IgnoreMember]
        private int? _hashCode;
        public override int GetHashCode()
        {
            if (_hashCode.HasValue) return _hashCode.Value;
            if (!IsFrozen) return CalcHashCode();
            _hashCode = CalcHashCode();
            return _hashCode.Value;
        }

    }

    [MessagePackObject]
    public partial class RightTriangle : Triangle, IRightTriangle, IEquatable<RightTriangle>
    {
        // Derived entities: 0

        public new const int EntityKey = 6;

        public new static RightTriangle Create(int entityKey, ReadOnlyMemory<byte> buffer)
        {
            return entityKey switch
            {
                _ => throw new ArgumentOutOfRangeException(nameof(entityKey), entityKey, null)
            };
        }

        protected override void OnFreeze()
        {
            base.OnFreeze();
            // todo freezable members
        }

        protected override IFreezable OnPartCopy() => new RightTriangle(this);

        public RightTriangle() { }
        public RightTriangle(IRightTriangle source, bool frozen = false) : base(source, frozen)
        {
            _Length = source.Length;
            _Height = source.Height;
        }

        [IgnoreMember]
        private Double _Length = default;
        [Key(201)]
        public Double Length
        {
            get => _Length;
            set => _Length = IfNotFrozen(ref value);
        }

        [IgnoreMember]
        private Double _Height = default;
        [Key(202)]
        public Double Height
        {
            get => _Height;
            set => _Height = IfNotFrozen(ref value);
        }


        public bool Equals(RightTriangle? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            if (!base.Equals(other)) return false;
            if (_Length != other.Length) return false;
            if (_Height != other.Height) return false;
            return true;
        }

        public override bool Equals(object? obj) => obj is RightTriangle other && Equals(other);
        public static bool operator ==(RightTriangle? left, RightTriangle? right) => left is not null ? left.Equals(right) : (right is null);
        public static bool operator !=(RightTriangle? left, RightTriangle? right) => left is not null ? !left.Equals(right) : (right is not null);

        private int CalcHashCode()
        {
            HashCode result = new HashCode();
            result.Add(base.GetHashCode());
            result.Add(_Length);
            result.Add(_Height);
            return result.ToHashCode();
        }

        [IgnoreMember]
        private int? _hashCode;
        public override int GetHashCode()
        {
            if (_hashCode.HasValue) return _hashCode.Value;
            if (!IsFrozen) return CalcHashCode();
            _hashCode = CalcHashCode();
            return _hashCode.Value;
        }

    }

    [MessagePackObject]
    [Union(Rectangle.EntityKey, typeof(Rectangle))]
    [Union(Square.EntityKey, typeof(Square))]
    public abstract partial class Quadrilateral { }
    public partial class Quadrilateral : Polygon, IQuadrilateral, IEquatable<Quadrilateral>
    {
        // Derived entities: 2
        // - Rectangle
        // - Square

        public new const int EntityKey = 7;

        public new static Quadrilateral Create(int entityKey, ReadOnlyMemory<byte> buffer)
        {
            return entityKey switch
            {
                Sandpit2.MessagePack.Rectangle.EntityKey => MessagePackSerializer.Deserialize<Sandpit2.MessagePack.Rectangle>(buffer, out var _),
                Sandpit2.MessagePack.Square.EntityKey => MessagePackSerializer.Deserialize<Sandpit2.MessagePack.Square>(buffer, out var _),
                _ => throw new ArgumentOutOfRangeException(nameof(entityKey), entityKey, null)
            };
        }

        protected override void OnFreeze()
        {
            base.OnFreeze();
            // todo freezable members
        }

        public Quadrilateral() { }
        public Quadrilateral(IQuadrilateral source, bool frozen = false) : base(source, frozen)
        {
        }


        public bool Equals(Quadrilateral? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            if (!base.Equals(other)) return false;
            return true;
        }

        public override bool Equals(object? obj) => obj is Quadrilateral other && Equals(other);
        public static bool operator ==(Quadrilateral? left, Quadrilateral? right) => left is not null ? left.Equals(right) : (right is null);
        public static bool operator !=(Quadrilateral? left, Quadrilateral? right) => left is not null ? !left.Equals(right) : (right is not null);

        private int CalcHashCode()
        {
            HashCode result = new HashCode();
            result.Add(base.GetHashCode());
            return result.ToHashCode();
        }

        [IgnoreMember]
        private int? _hashCode;
        public override int GetHashCode()
        {
            if (_hashCode.HasValue) return _hashCode.Value;
            if (!IsFrozen) return CalcHashCode();
            _hashCode = CalcHashCode();
            return _hashCode.Value;
        }

    }

    [MessagePackObject]
    public partial class Square : Quadrilateral, ISquare, IEquatable<Square>
    {
        // Derived entities: 0

        public new const int EntityKey = 8;

        public new static Square Create(int entityKey, ReadOnlyMemory<byte> buffer)
        {
            return entityKey switch
            {
                _ => throw new ArgumentOutOfRangeException(nameof(entityKey), entityKey, null)
            };
        }

        protected override void OnFreeze()
        {
            base.OnFreeze();
            // todo freezable members
        }

        protected override IFreezable OnPartCopy() => new Square(this);

        public Square() { }
        public Square(ISquare source, bool frozen = false) : base(source, frozen)
        {
            _Length = source.Length;
        }

        [IgnoreMember]
        private Double _Length = default;
        [Key(201)]
        public Double Length
        {
            get => _Length;
            set => _Length = IfNotFrozen(ref value);
        }


        public bool Equals(Square? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            if (!base.Equals(other)) return false;
            if (_Length != other.Length) return false;
            return true;
        }

        public override bool Equals(object? obj) => obj is Square other && Equals(other);
        public static bool operator ==(Square? left, Square? right) => left is not null ? left.Equals(right) : (right is null);
        public static bool operator !=(Square? left, Square? right) => left is not null ? !left.Equals(right) : (right is not null);

        private int CalcHashCode()
        {
            HashCode result = new HashCode();
            result.Add(base.GetHashCode());
            result.Add(_Length);
            return result.ToHashCode();
        }

        [IgnoreMember]
        private int? _hashCode;
        public override int GetHashCode()
        {
            if (_hashCode.HasValue) return _hashCode.Value;
            if (!IsFrozen) return CalcHashCode();
            _hashCode = CalcHashCode();
            return _hashCode.Value;
        }

    }

    [MessagePackObject]
    public partial class Rectangle : Quadrilateral, IRectangle, IEquatable<Rectangle>
    {
        // Derived entities: 0

        public new const int EntityKey = 9;

        public new static Rectangle Create(int entityKey, ReadOnlyMemory<byte> buffer)
        {
            return entityKey switch
            {
                _ => throw new ArgumentOutOfRangeException(nameof(entityKey), entityKey, null)
            };
        }

        protected override void OnFreeze()
        {
            base.OnFreeze();
            // todo freezable members
        }

        protected override IFreezable OnPartCopy() => new Rectangle(this);

        public Rectangle() { }
        public Rectangle(IRectangle source, bool frozen = false) : base(source, frozen)
        {
            _Length = source.Length;
            _Height = source.Height;
        }

        [IgnoreMember]
        private Double _Length = default;
        [Key(201)]
        public Double Length
        {
            get => _Length;
            set => _Length = IfNotFrozen(ref value);
        }

        [IgnoreMember]
        private Double _Height = default;
        [Key(202)]
        public Double Height
        {
            get => _Height;
            set => _Height = IfNotFrozen(ref value);
        }


        public bool Equals(Rectangle? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            if (!base.Equals(other)) return false;
            if (_Length != other.Length) return false;
            if (_Height != other.Height) return false;
            return true;
        }

        public override bool Equals(object? obj) => obj is Rectangle other && Equals(other);
        public static bool operator ==(Rectangle? left, Rectangle? right) => left is not null ? left.Equals(right) : (right is null);
        public static bool operator !=(Rectangle? left, Rectangle? right) => left is not null ? !left.Equals(right) : (right is not null);

        private int CalcHashCode()
        {
            HashCode result = new HashCode();
            result.Add(base.GetHashCode());
            result.Add(_Length);
            result.Add(_Height);
            return result.ToHashCode();
        }

        [IgnoreMember]
        private int? _hashCode;
        public override int GetHashCode()
        {
            if (_hashCode.HasValue) return _hashCode.Value;
            if (!IsFrozen) return CalcHashCode();
            _hashCode = CalcHashCode();
            return _hashCode.Value;
        }

    }
}
