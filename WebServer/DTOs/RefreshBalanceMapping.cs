namespace HamsterCoin.DTO
{
    public class RefreshBalanceRequest
    {
        public decimal Balance { get; set; }
        public string JWTtoken { get; set; }
    }
}