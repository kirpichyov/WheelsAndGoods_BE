namespace WheelsAndGoods.Core.Models.Entities;

public class ResetPasswordCode : EntityBase<Guid>
{
    public string Email { get; set; }
    public DateTime CreatedAtUtc { get; set; }
    public string Code { get; set; }
}
