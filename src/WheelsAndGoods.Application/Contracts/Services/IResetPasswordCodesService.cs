using WheelsAndGoods.Application.Models.Auth;

namespace WheelsAndGoods.Application.Contracts.Services;

public interface IResetPasswordCodesService
{
    public Task SendResetPasswordCodeEmail(ResetPasswordRequest request);
}
