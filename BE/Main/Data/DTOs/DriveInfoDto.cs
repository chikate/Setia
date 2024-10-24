namespace Main.Data.DTOs;

public class DriveInfoDto
{
    public required string Name { get; set; }
    public required string VolumeLabel { get; set; }
    public required string DriveType { get; set; }
    public required long AvailableFreeSpace { get; set; }
    public required long TotalSize { get; set; }
}