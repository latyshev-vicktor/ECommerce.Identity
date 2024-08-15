using ECommerce.Domain.SeedWorks;

namespace ECommerce.Domain.Entities
{
    public class RefreshToken : BaseEntity
    {
        public Guid UserId { get; private set; }
        public User User { get; private set; }
        public string TokenHash {  get; private set; } = string.Empty;
        public DateTimeOffset Ts { get; private set; }
        public DateTimeOffset ExpiryDate { get; private set; }
        public DateTimeOffset? RevokedDate { get; private set; }

        public bool IsExpired => DateTimeOffset.UtcNow >= ExpiryDate;

        public bool IsRevoked => RevokedDate != null;

        #region Конструкторы
        protected RefreshToken() { }

        public RefreshToken(
            Guid userId, 
            string tokenHash,
            DateTimeOffset ts,
            DateTimeOffset expiryDate)
        {
            UserId = userId;
            TokenHash = tokenHash;
            Ts = ts;
            ExpiryDate = expiryDate;
            CreatedDate = DateTimeOffset.UtcNow;
            ModifyDate = DateTimeOffset.UtcNow;
        }
        #endregion

        #region DDD методы
        public void Revoked()
        {
            RevokedDate = DateTimeOffset.UtcNow;
            MakeModify();
        }
        #endregion
    }
}
