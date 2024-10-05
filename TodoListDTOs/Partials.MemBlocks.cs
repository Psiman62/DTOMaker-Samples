
using System;

namespace TodoListDTOs.MemBlocks
{
    public partial class AllTypesExplicit : IAllTypesExplicit
    {
        int? IAllTypesExplicit.OptionalField07 => this.Field01 ? this.Field07 : null;

#if NET6_0_OR_GREATER
        Half? IAllTypesExplicit.OptionalField10 => this.Field01 ? this.Field10 : null;
#endif
    }
    public partial class AllTypesSequential : IAllTypesSequential
    {
        int? IAllTypesSequential.OptionalField07 => this.Field01 ? this.Field07 : null;

#if NET6_0_OR_GREATER
        Half? IAllTypesSequential.OptionalField10 => this.Field01 ? this.Field10 : null;
#endif
    }
}
