using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


using HamsterCoin.Domain{

    [Table("user_details")]
    
    public class User_Details{
        [Key]
        [Required]
        [Column("id")]
        public int Id {get; set;}

        [Required]
        [Column("nickname")]
        [MaxLength(50)]
        public string Nickname {get; set;}
    
        [Required]
        [Column("promocode")]
        [MaxLength(50)]

        public string Promocode{get;set;}

        [Column("balance")]

        public decimal Balance {get;set}

        [Column("birth_date")]
        public DateTime birth_date{get;set}

    }
}