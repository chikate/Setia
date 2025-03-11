namespace Main.Modules.Gov.Models;

public class GetFilterDTO<T>
{
    public List<T> Items { get; set; } = new();
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
}