namespace SubhashPortfolio.Application.Common.Interfaces;

public interface IEmailService
{
    Task SendContactNotificationAsync(
        string senderName,
        string senderEmail,
        string? phone,
        string subject,
        string message,
        CancellationToken cancellationToken = default);

    Task SendAutoReplyAsync(
        string recipientName,
        string recipientEmail,
        string originalSubject,
        CancellationToken cancellationToken = default);
}