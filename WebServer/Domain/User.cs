using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HamsterCoin.Domain
{

    [Table("users")]
    public class User
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        [Column("Email")]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; } = null!;

        [Required]
        [Column("password")]
        [MaxLength(255)]
        public string Password { get; set; } = null!;

        [Required]
        [Column("nickname")]
        [MaxLength(50)]
        public string Nickname { get; set; }

        [Required]
        [Column("promocode")]
        [MaxLength(50)]
        public string? Promocode { get; set; }

        public string Nickname { get; set; } = null!;
        
        [Column("balance")]
        public decimal Balance { get; set; }

        [Column("birth_date")]
        public DateOnly BirthDate { get; set; }
    }
}
