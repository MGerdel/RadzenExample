namespace RadzenExample.Models;

public sealed record Model
{
    public string Name { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public Status Status { get; set; }
}