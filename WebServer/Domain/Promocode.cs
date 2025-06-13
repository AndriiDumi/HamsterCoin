using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HamsterCoin.Domain
{
    public class Promocode
    {
        [Key]
        public int id { get; set; }
        [Required]
        [ForeignKey(nameof(User))]
        [Column("user_id")]
        public long UserId { get; set; }

        [NotMapped]
        public User User { get; set; }

        [Required]
        [Column("promocode")]
        [MaxLength(50)]
        public string promocode { get; set; } = null!;
    }
}