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
        public int Id { get; set; }

        [Required]
        [Column("mail")]
        [EmailAddress]
        [MaxLength(100)]
        public string Mail { get; set; }

        [Required]
        [Column("password")]
        [MaxLength(255)]
        public string Password { get; set; }

    }
}
