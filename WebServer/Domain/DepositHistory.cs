using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HamsterCoin.Domain
{

    [Table("dep_history")]
    public class DepositHistory
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
        [Column("sum_dep")]
        public decimal SumDep { get; set; }

        [Required]
        [Column("date_dep")]
        public DateTime DateDep { get; set; }
    }

}