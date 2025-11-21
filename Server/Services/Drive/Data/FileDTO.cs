using Microsoft.AspNetCore.Mvc;

namespace Modules.Drive;

/// <summary>
/// DTO for file content result.
/// </summary>
public class FileDTO : FileContentResult
{
    public FileDTO(byte[] fileContents, string contentType) : base(fileContents, contentType) { }
    public required Guid Registry { get; set; }
    public required Guid Owner { get; set; }
    public required string Path { get; set; }

}