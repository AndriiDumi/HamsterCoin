using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HamsterCoin.Domain{
    [Table("cards")]
    public class Card
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Column("number")]
        [Required]
        [MaxLength(12)]
        public string Number { get; set; } = null!;

        [Column("date")]
        [Required]
        [MaxLength(4)]
        public string Date { get; set; } = null!;

        [Column("cvv")]
        [Required]
        [MaxLength(3)]
        public string Cvv { get; set; } = null!;
    }
}