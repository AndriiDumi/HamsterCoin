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

    [Required] 
    [Column("name")] 
    [MaxLength(50)] 
    public string Name { get; set; } = null!;

    [Required] 
    [Column("isMultiplayer")] 
    public bool IsMultiplayer { get; set; } 

    [Required] 
    [Column("count_players")] 
    public int CountPlayers { get; set; }
}

}
