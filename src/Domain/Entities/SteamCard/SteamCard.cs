using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("SteamCard")]
    public class SteamCard
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        //[ForeignKey("User")]        
        //public Guid? UserId { get; set; }
        //public virtual User? User { get; set; }
        public string Key { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
        [ForeignKey("SteamCardCategory")]
        public Guid? SteamCardCategoryId { get; set; }
        public virtual SteamCardCategory? SteamCardCategory { get; set; }
    }
}
