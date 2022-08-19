namespace WheelsAndGoods.Core.Models.Entities;

public class RefreshToken : EntityBase<Guid>
{
    public RefreshToken() 
        : base(Guid.NewGuid())
    {
        
    }
    
    public Guid AccessTokenId { get; set; }
    public DateTime CreatedAtUtc { get; set; }
    public bool IsInvalidated { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }

}
