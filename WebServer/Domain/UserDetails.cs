using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


namespace HamsterCoin.Domain
{

    [Table("user_details")]

    public class UserDetails
    {
        [Key]
        [Required]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(User))]
        [Column("user_id")]
        public int UserId { get; set; }

        [NotMapped] // Це поле не буде збережене в БД
        public User? User { get; set; } //навігаційне поле

        [Required]
        [Column("nickname")]
        [MaxLength(50)]
        public string Nickname { get; set; }

        [Required]
        [Column("promocode")]
        [MaxLength(50)]
        public string Promocode { get; set; }

        [Column("balance")]
        public decimal Balance { get; set; }

        [Column("birth_date")]
        public DateTime BirthDate { get; set; }

    }
}
