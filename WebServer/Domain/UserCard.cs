using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HamsterCoin.Domain
{
   [Table("user_cards")]
   public class UserCard
   {
      [Key]
      [Required]
      [Column("id")]
      public long id{get;set;}
      [Required]
      [ForeignKey(nameof(User))]
      [Column("user_id")]
      public long UserId { get; set; }

      [NotMapped]    
      public User User { get; set; }

      [Required]
      [ForeignKey(nameof(Card))]
      [Column("card_id")]
      public long? CardId { get; set; }

      [NotMapped]
      public Card Card { get; set; }
   }
}
