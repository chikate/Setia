namespace Setia.Structs
{
    public class DefinitionStruct : AuditStruct
    {
        public bool Active { get; set; } = true;
        public bool Deleted { get; set; } = false;
    }
}