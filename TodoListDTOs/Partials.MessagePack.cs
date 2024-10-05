using MessagePack;
using System;

namespace TodoListDTOs.MessagePack
{
    public partial class AllTypesSequential : IAllTypesSequential
    {
        [IgnoreMember]
        int? IAllTypesSequential.OptionalField07 => this.Field01 ? this.Field07 : null;

#if NET6_0_OR_GREATER
        [IgnoreMember]
        Half? IAllTypesSequential.OptionalField10 => this.Field01 ? this.Field10 : null;
#endif
    }
}
