namespace Domain.DTO;

public class UserDTO : IEntityDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public virtual IEnumerable<SteamCard> SteamCard { get; set; } = [];
    public DateTime CreatedAt { get; set; }
    public bool Active { get; set; }
    public Role Role { get; set; }
}