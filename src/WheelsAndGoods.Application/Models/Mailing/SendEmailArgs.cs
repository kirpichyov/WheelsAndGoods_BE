namespace WheelsAndGoods.Application.Models.Mailing;

public class SendEmailArgs
{
    public string RecipientEmail { get; set; }
    public string Subject { get; set; }
    public string PlainTextContent { get; set; }
    public string HtmlContent { get; set; }
}
