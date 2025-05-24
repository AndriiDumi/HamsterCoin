
namespace HamsterCoin.DTO
{
    public class CardDTO
    {
        public long UserId { get; set; }
        public required string Number { get; set; }
        public required string Date { get; set; }
        public required string Cvv { get; set; }
    }
}
