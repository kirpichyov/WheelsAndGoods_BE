using SendGrid;
using WheelsAndGoods.Application.Models.Mailing;

namespace WheelsAndGoods.Application.Contracts.Services;

public interface IEmailSenderService
{
    Task<Response> Send(SendEmailArgs args);
}
