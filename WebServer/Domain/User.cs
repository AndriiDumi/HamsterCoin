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
        public string Email { get; set; }

        [Required]
        [Column("password")]
        [MaxLength(255)]
        public string Password { get; set; }

        [Required]
        [Column("nickname")]
        [MaxLength(50)]
        public string Nickname { get; set; }

        [Required]
        [Column("promocode")]
        [MaxLength(50)]
        public string? Promocode { get; set; }

        [Column("balance")]
        public decimal Balance { get; set; }

        [Column("birth_date")]
        public DateOnly BirthDate { get; set; }
    }
}
