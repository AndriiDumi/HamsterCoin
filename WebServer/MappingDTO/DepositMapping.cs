using Microsoft.EntityFrameworkCore;
using HamsterCoin.Domain;

namespace HamsterCoin.Mapping
{
    public class DepositDTO
    {
        public long UserId { get; set; }
        public decimal SumDep { get; set; }
        public DateTime DateDep { get; set; }
    }
}
