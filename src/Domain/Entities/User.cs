using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("user")]
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
        [Required]
        public virtual ICollection<SteamCard> SteamCards { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Active { get; set; }
    }
}