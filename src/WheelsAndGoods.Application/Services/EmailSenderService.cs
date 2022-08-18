using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using WheelsAndGoods.Application.Contracts.Services;
using WheelsAndGoods.Application.Models.Mailing;
using WheelsAndGoods.Application.Options;

namespace WheelsAndGoods.Application.Services;

public class EmailSenderService : IEmailSenderService
{
    private readonly ISendGridClient _client;
    private readonly SendGridOptions _options;

    public EmailSenderService(IOptions<SendGridOptions> options)
    {
        _options = options.Value;
        _client = new SendGridClient(options.Value.ApiKey);
    }

    public async Task<Response> Send(SendEmailArgs args)
    {
        var from = new EmailAddress(_options.SenderEmail, "Wheels and Goods notifier");
        var to = new EmailAddress(args.RecipientEmail);
        var msg = MailHelper.CreateSingleEmail(from, to, args.Subject, args.PlainTextContent, args.HtmlContent);
        return await _client.SendEmailAsync(msg).ConfigureAwait(false);
    }
}
