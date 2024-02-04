namespace Setia.Structs
{
    public class DefinitionDto : BaseAuditDto
    {
        public bool Active { get; set; } = true;
        public bool Deleted { get; set; } = false;
    }
}