using MessagePack;

namespace TodoListDTOs.MessagePack
{
    public partial class AllTypesSequential : IAllTypesSequential
    {
        [IgnoreMember]
        int? IAllTypesSequential.OptionalField08 => this.Field01 ? this.Field08 : null;
    }
}
