namespace Setia.Models.Structs
{
    public class BaseModel : BaseAudit
    {
        public List<string>? Tags { get; set; } = null;
    }
}