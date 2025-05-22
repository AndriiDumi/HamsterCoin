using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HamsterCoin.Domain
{
    [Table("refreshtokens")]
    public class RefreshToken
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Required]
        [Column("token")]
        public required string Token { get; set; }

        [Required]
        [Column("expiresAt")]
        public required DateTime ExpiresAt { get; set; }

        [Required]
        [Column("isRevoked")]
        public bool IsRevoked { get; set; } = false;

        [Required]
        [Column("user_id")]
        public required long UserId { get; set; }

        public bool IsExpired => DateTime.UtcNow >= ExpiresAt;
        public bool IsActive => !IsRevoked && !IsExpired;
    }
}