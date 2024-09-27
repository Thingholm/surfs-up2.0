namespace Api.Models;

public class BoardEquipment
{
    public int Id { get; set; }
    public required Board Board { get; set; }
    public required Equipment Equipment { get; set; }
}