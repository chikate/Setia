namespace Setia.Models.Structs
{
    public class DefinitionStruct : BaseAuditStruct
    {
        public bool Active { get; set; } = true;
        public bool Deleted { get; set; } = false;
        //public List<string> Tags { get; set; } = new List<string>();
    }
}