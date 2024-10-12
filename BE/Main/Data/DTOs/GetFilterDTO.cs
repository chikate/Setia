namespace Main.Data.DTOs;

public class GetFilterDTO<T>
{
    public List<T> Item { get; set; } = new();
    public int? PageSize { get; set; } = null;
    public int? PageNumber { get; set; } = null;
}