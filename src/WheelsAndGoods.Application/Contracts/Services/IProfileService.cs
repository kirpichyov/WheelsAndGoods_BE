using WheelsAndGoods.Application.Models.User;
using WheelsAndGoods.Application.Models.User.Responses;

namespace WheelsAndGoods.Application.Contracts.Services
{
    public interface IProfileService
    {
        Task ChangePassword(ChangePasswordRequest request);
        Task<UpdateAvatarResponse> UpdateAvatar();
    }
}
