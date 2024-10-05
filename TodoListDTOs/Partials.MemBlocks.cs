
namespace TodoListDTOs.MemBlocks
{
    public partial class AllTypesExplicit : IAllTypesExplicit
    {
        int? IAllTypesExplicit.OptionalField08 => this.Field01 ? this.Field08 : null;
    }
    public partial class AllTypesSequential : IAllTypesSequential
    {
        int? IAllTypesSequential.OptionalField08 => this.Field01 ? this.Field08 : null;
    }
}
