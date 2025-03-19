using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HamsterCoin.Domain{

    [Table("withdraw_history")]
    public class WithdrawHistory
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey(nameof(User))]
        [Column("user_id")]
        public long UserId { get; set; }

        [NotMapped]
        public User? User { get; set; }

        [Required]
        [Column("sum_withdraw")]
        public decimal SumWithdraw { get; set; }

        [Required]
        [Column("date_withdraw")]
        public DateTime DateWithdraw { get; set; }
    }
}