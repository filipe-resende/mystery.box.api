namespace Domain.Entities;

[Table("User")]
public class User
{
    [Key]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Name is required")]
    [StringLength(60, ErrorMessage = "Name can't be longer than 60 characters")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; }

    [Required(ErrorMessage = "CPF is required")]
    public string CPF { get; set; }

    public string Phone { get; set; }

    public virtual ICollection<SteamCard> SteamCards { get; set; }
    public virtual ICollection<Payment> Payments { get; set; }

    public DateTime CreatedAt { get; set; }

    public bool Active { get; set; }
}
