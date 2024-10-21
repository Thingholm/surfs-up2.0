using System.Reflection.Metadata;

namespace Web.Models;

public class Board
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public double Length { get; set; }
    public double Width { get; set; }
    public double Thickness { get; set; }
    public double Volume { get; set; }
    public double Price { get; set; }
    public BoardType BoardType { get; set; }
    public string ImageUrl { get; set; } = null!;

    public ICollection<BoardEquipment> BoardEquipment { get; set; }
}