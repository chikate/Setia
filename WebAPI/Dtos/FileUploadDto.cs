using System.ComponentModel.DataAnnotations;

namespace Setia.Structs
{
    public class FileUploadDto : BaseAuditStruct
    {
        public List<IFormFile> Files { get; set; }
        public string? Details { get; set; } = null;
    }
}