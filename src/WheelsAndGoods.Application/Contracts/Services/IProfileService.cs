using WheelsAndGoods.Application.Models.User;

namespace WheelsAndGoods.Application.Contracts.Services
{
    public interface IProfileService
    {
        Task ChangePassword(ChangePasswordRequest request);
    }
}
