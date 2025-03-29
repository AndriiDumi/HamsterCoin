
using Microsoft.EntityFrameworkCore;
using HamsterCoin.Domain;

namespace HamsterCoin.Mapping
{
    public class WithdrawDTO
    {
        public long UserId { get; set; }
        public decimal SumWithdraw { get; set; }
        public DateTime DateWithdraw { get; set; }
    }
}
