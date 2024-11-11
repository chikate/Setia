namespace Main.Data.DTOs;

public class GetFilterDTO<T>
{
    public List<T> Items { get; set; } = new();
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
}