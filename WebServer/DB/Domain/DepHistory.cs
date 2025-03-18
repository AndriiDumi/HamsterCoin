using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HamsterCoin.Domain{

[Table("dep_history")]
public class Deposit
{
    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column("user_id")]
    public long? UserId { get; set; }

    [Required]
    [Column("sum_dep")]
    public decimal SumDep { get; set; }

    [Required]
    [Column("date_dep")]
    public DateTime DateDep { get; set; }
}

}