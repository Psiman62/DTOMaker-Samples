
using System;

namespace TodoListDTOs.MemBlocks
{
    public partial class AllTypesExplicit : IAllTypesExplicit
    {
        int? IAllTypesExplicit.OptionalField07 => this.Field01 ? this.Field07 : null;
    }
    public partial class AllTypesSequential : IAllTypesSequential
    {
        int? IAllTypesSequential.OptionalField07 => this.Field01 ? this.Field07 : null;
    }
}
