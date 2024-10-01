using System.Text.Json.Serialization;

namespace Api.Models;

public class BoardEquipment
{
    public int Id { get; set; }
    [JsonIgnore]
    public Board? Board { get; set; } = null!;
    public Equipment Equipment { get; set; } = null!;
}