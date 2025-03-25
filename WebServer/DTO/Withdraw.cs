
using Microsoft.EntityFrameworkCore;
using HamsterCoin.Domain;

namespace HamsterCoin.DTO
{
    public class WithdrawDTO
    {
        public long UserId { get; set; }
        public User? User { get; set; }
        public decimal SumWithdraw { get; set; }
        public DateTime DateWithdraw { get; set; }
    }
}
