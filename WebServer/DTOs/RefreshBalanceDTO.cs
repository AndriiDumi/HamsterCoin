namespace HamsterCoin.DTO
{
    public class RefreshBalanceRequest
    {
        public decimal Balance { get; set; }
        public required string JWTtoken { get; set; }
    }
}
