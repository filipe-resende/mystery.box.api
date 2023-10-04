using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("steamcard")]
    public class SteamCard
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Active { get; set; }
    }
}
