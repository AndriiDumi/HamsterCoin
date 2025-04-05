using HamsterCoin.Domain;

namespace HamsterCoin.Mapping
{
    public class CardDTO
    {
        public long UserId{get;set;}
        public string Number { get; set; }
        public string Date { get; set; }
        public string Cvv { get; set; }
    }
}
