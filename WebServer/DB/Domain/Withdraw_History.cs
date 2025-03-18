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

        [Column("user_id")]
        [Required]
        public long UserId { get; set; }

        [Column("sum_withdraw")]
        [Required]
        public decimal SumWithdraw { get; set; }

        [Column("date_withdraw")]
        [Required]
        public DateTime DateWithdraw { get; set; }
    }
}