using Domain.Entities;

namespace Domain.DTO;

public class UserDTO : IEntityDTO
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string CPF { get; set; }
    public string Phone { get; set; }
    public string Password { get; set; }
    public virtual IEnumerable<SteamCard> SteamCard { get; set; } = [];
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool Active { get; set; }
}