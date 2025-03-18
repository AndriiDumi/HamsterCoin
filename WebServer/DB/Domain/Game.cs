using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HamsterCoin.Domain{

[Table("games")] 
public class Game
{
    [Key] 
    [Column("game_id")] 
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
    public long GameId { get; set; } 

    [Column("name")] 
    [Required] 
    [MaxLength(50)] 
    public string Name { get; set; } = null!;

    [Column("isMultiplayer")] 
    [Required] 
    public bool IsMultiplayer { get; set; } 

    [Column("count_players")] 
    [Required] 
    public int CountPlayers { get; set; }
}

}