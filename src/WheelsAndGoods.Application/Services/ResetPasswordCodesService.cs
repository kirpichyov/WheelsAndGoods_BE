using System.Text;
using Microsoft.Extensions.Options;
using WheelsAndGoods.Application.Contracts.Services;
using WheelsAndGoods.Application.Models.Auth;
using WheelsAndGoods.Application.Models.Mailing;
using WheelsAndGoods.Application.Options;
using WheelsAndGoods.Core.Exceptions;
using WheelsAndGoods.Core.Models.Entities;
using WheelsAndGoods.DataAccess.Contracts;

namespace WheelsAndGoods.Application.Services;

public class ResetPasswordCodesService : IResetPasswordCodesService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IKeysGeneratorService _keysGeneratorService;
    private readonly IEmailSenderService _emailSenderService;
    private readonly ResetPasswordCodeOptions _options;

    public ResetPasswordCodesService(
        IUnitOfWork unitOfWork, 
        IEmailSenderService emailSenderService, 
        IKeysGeneratorService keysGeneratorService, 
        IOptions<ResetPasswordCodeOptions> options)
    {
        _unitOfWork = unitOfWork;
        _emailSenderService = emailSenderService;
        _keysGeneratorService = keysGeneratorService;
        _options = options.Value;
    }

    public async Task SendResetPasswordCodeEmail(ResetPasswordRequest request)
    {
        string emailMessage = 
            $"You have requested a password reset. Follow the link to reset your password: {_options.Url}";
        var user = _unitOfWork.Users.GetByEmail(request.Email);

        if (user is null)
        {
            throw new NotFoundException("User not found"); 
        }

        var resetCode = _keysGeneratorService.Generate(32);

        var resetPasswordCode = new ResetPasswordCode()
        {
            Code = resetCode,
            Email = request.Email,
            CreatedAtUtc = DateTime.UtcNow
        };

        await _unitOfWork.CommitTransactionAsync(() =>
        {
            _unitOfWork.ResetPasswordCodes.Add(resetPasswordCode);
        });
        
        await _emailSenderService.Send(new SendEmailArgs()
        {
            RecipientEmail = request.Email,
            PlainTextContent = emailMessage + $"?code={resetCode}",
            Subject = "Reset password"
        });
    }
}
