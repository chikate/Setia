namespace Setia.Structs
{
    public class DefinitionStruct : BaseAuditStruct
    {
        public bool Active { get; set; } = true;
        public bool Deleted { get; set; } = false;
    }
}