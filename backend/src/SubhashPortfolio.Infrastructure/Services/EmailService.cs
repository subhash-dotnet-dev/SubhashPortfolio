using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using SubhashPortfolio.Application.Common;
using SubhashPortfolio.Application.Common.Interfaces;

namespace SubhashPortfolio.Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly EmailSettings _settings;
    private readonly ILogger<EmailService> _logger;

    public EmailService(IOptions<EmailSettings> settings, ILogger<EmailService> logger)
    {
        _settings = settings.Value;
        _logger = logger;
    }

    public async Task SendContactNotificationAsync(
        string senderName, string senderEmail, string? phone,
        string subject, string message, CancellationToken cancellationToken = default)
    {
        if (!_settings.Enabled)
        {
            _logger.LogInformation("Email notifications disabled");
            return;
        }

        try
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(_settings.SenderName, _settings.SenderEmail));
            email.To.Add(new MailboxAddress("Subhash Yadav", _settings.RecipientEmail));
            email.ReplyTo.Add(new MailboxAddress(senderName, senderEmail));
            email.Subject = $"💬 New Message from {senderName} — {subject}";
            email.Priority = MessagePriority.Urgent;

            var initials = GetInitials(senderName);
            var timestamp = DateTime.UtcNow.ToString("dddd, dd MMMM yyyy 'at' HH:mm 'UTC'");
            var replyMailto = $"mailto:{senderEmail}?subject=Re: {Uri.EscapeDataString(subject)}";

            var html = BuildPremiumTemplate(
                senderName, senderEmail, phone, subject, message,
                initials, timestamp, replyMailto);

            email.Body = new TextPart("html") { Text = html };

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_settings.SmtpHost, _settings.SmtpPort, SecureSocketOptions.StartTls, cancellationToken);
            await smtp.AuthenticateAsync(_settings.SenderEmail, _settings.AppPassword, cancellationToken);
            await smtp.SendAsync(email, cancellationToken);
            await smtp.DisconnectAsync(true, cancellationToken);

            _logger.LogInformation("Contact notification sent to {Recipient}", _settings.RecipientEmail);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send contact notification email");
        }
    }

    private static string GetInitials(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) return "?";
        var parts = name.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length == 1) return parts[0][..1].ToUpper();
        return $"{parts[0][..1]}{parts[^1][..1]}".ToUpper();
    }

    private static string BuildPremiumTemplate(
        string senderName, string senderEmail, string? phone,
        string subject, string message, string initials,
        string timestamp, string replyMailto)
    {
        return $@"
<!DOCTYPE html>
<html lang=""en"">
<head>
<meta charset=""UTF-8"">
<meta name=""viewport"" content=""width=device-width,initial-scale=1.0"">
<title>New Contact Message</title>
</head>
<body style=""margin:0;padding:0;background-color:#050914;font-family:-apple-system,BlinkMacSystemFont,'Segoe UI',Roboto,'Helvetica Neue',Arial,sans-serif;-webkit-font-smoothing:antialiased;"">

<!-- Outer wrapper -->
<table role=""presentation"" cellpadding=""0"" cellspacing=""0"" border=""0"" width=""100%"" style=""background-color:#050914;padding:32px 16px;"">
<tr><td align=""center"">

<!-- Main card -->
<table role=""presentation"" cellpadding=""0"" cellspacing=""0"" border=""0"" width=""600"" style=""max-width:600px;width:100%;background-color:#0a1224;border-radius:20px;overflow:hidden;box-shadow:0 20px 60px rgba(0,0,0,0.5),0 0 0 1px rgba(59,130,246,0.15);"">

<!-- ═══ HERO HEADER ═══ -->
<tr><td style=""background:linear-gradient(135deg,#0f172a 0%,#050914 100%);padding:0;position:relative;"">
  <!-- Top accent bar -->
  <table role=""presentation"" width=""100%"" cellpadding=""0"" cellspacing=""0"">
    <tr><td style=""height:4px;background:linear-gradient(90deg,#3b82f6 0%,#22d3ee 50%,#8b5cf6 100%);""></td></tr>
  </table>

  <!-- Header content -->
  <table role=""presentation"" width=""100%"" cellpadding=""0"" cellspacing=""0"">
    <tr><td style=""padding:36px 36px 28px;"">

      <!-- Brand row -->
      <table role=""presentation"" width=""100%"" cellpadding=""0"" cellspacing=""0"">
        <tr>
          <td align=""left"" valign=""middle"">
            <table role=""presentation"" cellpadding=""0"" cellspacing=""0"">
              <tr>
                <td style=""width:44px;height:44px;background:linear-gradient(135deg,#3b82f6,#22d3ee);border-radius:12px;text-align:center;vertical-align:middle;font-weight:900;font-size:16px;color:#ffffff;letter-spacing:-0.5px;box-shadow:0 8px 24px rgba(59,130,246,0.4);"">
                  SY
                </td>
                <td style=""padding-left:12px;vertical-align:middle;"">
                  <div style=""font-size:14px;font-weight:700;color:#ffffff;letter-spacing:-0.3px;line-height:1.2;"">Subhash Yadav</div>
                  <div style=""font-size:11px;color:#22d3ee;font-weight:600;letter-spacing:0.5px;margin-top:2px;"">.NET FULL STACK DEVELOPER</div>
                </td>
              </tr>
            </table>
          </td>
          <td align=""right"" valign=""middle"">
            <span style=""display:inline-block;padding:6px 14px;background:rgba(16,185,129,0.12);border:1px solid rgba(16,185,129,0.3);border-radius:999px;font-size:10px;font-weight:800;color:#10b981;letter-spacing:1.5px;text-transform:uppercase;"">
              ● NEW
            </span>
          </td>
        </tr>
      </table>

      <!-- Title -->
      <div style=""margin-top:32px;"">
        <div style=""font-size:11px;font-weight:800;color:#3b82f6;letter-spacing:2.5px;text-transform:uppercase;margin-bottom:8px;"">Incoming Message</div>
        <h1 style=""margin:0;font-size:32px;font-weight:900;color:#ffffff;letter-spacing:-1px;line-height:1.1;"">
          Someone wants to <span style=""background:linear-gradient(135deg,#22d3ee,#3b82f6);-webkit-background-clip:text;-webkit-text-fill-color:transparent;background-clip:text;"">connect</span>
        </h1>
        <div style=""margin-top:10px;font-size:13px;color:#94a3b8;font-weight:500;"">{timestamp}</div>
      </div>

    </td></tr>
  </table>
</td></tr>

<!-- ═══ SENDER INFO CARD ═══ -->
<tr><td style=""padding:0 36px;"">
  <table role=""presentation"" width=""100%"" cellpadding=""0"" cellspacing=""0"" style=""background:linear-gradient(135deg,rgba(59,130,246,0.08) 0%,rgba(34,211,238,0.05) 100%);border:1px solid rgba(59,130,246,0.2);border-radius:16px;overflow:hidden;"">
    <tr><td style=""padding:20px;"">

      <!-- Avatar + Name -->
      <table role=""presentation"" width=""100%"" cellpadding=""0"" cellspacing=""0"">
        <tr>
          <td valign=""middle"" style=""width:56px;"">
            <div style=""width:56px;height:56px;border-radius:14px;background:linear-gradient(135deg,#3b82f6,#22d3ee);text-align:center;line-height:56px;font-size:20px;font-weight:900;color:#ffffff;letter-spacing:-1px;box-shadow:0 8px 20px rgba(59,130,246,0.4);"">
              {initials}
            </div>
          </td>
          <td valign=""middle"" style=""padding-left:16px;"">
            <div style=""font-size:18px;font-weight:800;color:#ffffff;letter-spacing:-0.4px;line-height:1.2;"">{senderName}</div>
            <div style=""font-size:13px;color:#94a3b8;margin-top:4px;font-weight:500;"">{senderEmail}</div>
          </td>
        </tr>
      </table>

      <!-- Contact details grid -->
      <table role=""presentation"" width=""100%"" cellpadding=""0"" cellspacing=""0"" style=""margin-top:20px;border-top:1px solid rgba(148,163,184,0.12);padding-top:16px;"">
        <tr>
          <td valign=""top"" style=""width:50%;padding-right:8px;"">
            <div style=""font-size:10px;font-weight:800;color:#64748b;letter-spacing:1.5px;text-transform:uppercase;margin-bottom:6px;"">EMAIL</div>
            <a href=""mailto:{senderEmail}"" style=""font-size:13px;color:#22d3ee;text-decoration:none;font-weight:600;word-break:break-all;"">{senderEmail}</a>
          </td>
          <td valign=""top"" style=""width:50%;padding-left:8px;"">
            <div style=""font-size:10px;font-weight:800;color:#64748b;letter-spacing:1.5px;text-transform:uppercase;margin-bottom:6px;"">PHONE</div>
            <div style=""font-size:13px;color:#e2e8f0;font-weight:600;"">{(string.IsNullOrWhiteSpace(phone) ? "Not provided" : phone)}</div>
          </td>
        </tr>
      </table>

    </td></tr>
  </table>
</td></tr>

<!-- ═══ SUBJECT ═══ -->
<tr><td style=""padding:28px 36px 0;"">
  <div style=""font-size:10px;font-weight:800;color:#64748b;letter-spacing:2px;text-transform:uppercase;margin-bottom:8px;"">SUBJECT</div>
  <div style=""font-size:20px;font-weight:800;color:#ffffff;letter-spacing:-0.4px;line-height:1.3;"">{subject}</div>
</td></tr>

<!-- ═══ MESSAGE ═══ -->
<tr><td style=""padding:24px 36px 0;"">
  <div style=""font-size:10px;font-weight:800;color:#64748b;letter-spacing:2px;text-transform:uppercase;margin-bottom:10px;"">MESSAGE</div>
  <div style=""background:rgba(15,23,42,0.6);border-left:4px solid #3b82f6;border-radius:0 12px 12px 0;padding:20px 24px;font-size:15px;line-height:1.7;color:#cbd5e1;font-weight:400;white-space:pre-wrap;word-wrap:break-word;"">{message}</div>
</td></tr>

<!-- ═══ CTA BUTTONS ═══ -->
<tr><td style=""padding:32px 36px 0;"">
  <table role=""presentation"" width=""100%"" cellpadding=""0"" cellspacing=""0"">
    <tr>
      <td align=""left"">
        <a href=""{replyMailto}"" style=""display:inline-block;padding:14px 28px;background:linear-gradient(135deg,#3b82f6 0%,#2563eb 100%);border-radius:12px;font-size:14px;font-weight:800;color:#ffffff;text-decoration:none;letter-spacing:0.3px;box-shadow:0 10px 24px rgba(59,130,246,0.5);"">
          Reply Now →
        </a>
      </td>
      <td align=""right"" valign=""middle"">
        <a href=""mailto:{senderEmail}"" style=""font-size:13px;color:#94a3b8;text-decoration:none;font-weight:600;"">
          Save Contact
        </a>
      </td>
    </tr>
  </table>
</td></tr>

<!-- ═══ DIVIDER ═══ -->
<tr><td style=""padding:32px 36px 0;"">
  <div style=""height:1px;background:linear-gradient(90deg,transparent,rgba(59,130,246,0.3),transparent);""></div>
</td></tr>

<!-- ═══ FOOTER ═══ -->
<tr><td style=""padding:24px 36px 32px;"">
  <table role=""presentation"" width=""100%"" cellpadding=""0"" cellspacing=""0"">
    <tr>
      <td valign=""top"" align=""left"" style=""width:50%;"">
        <div style=""font-size:11px;color:#475569;font-weight:600;line-height:1.6;letter-spacing:0.3px;"">
          📧 Automated notification<br>
          from your portfolio contact form
        </div>
      </td>
      <td valign=""top"" align=""right"" style=""width:50%;"">
        <div style=""font-size:10px;color:#334155;font-weight:700;letter-spacing:1px;text-transform:uppercase;"">
          SUBHASH PORTFOLIO
        </div>
        <div style=""font-size:10px;color:#475569;margin-top:6px;font-weight:500;"">
          subhash.dev79@gmail.com
        </div>
      </td>
    </tr>
  </table>
</td></tr>

<!-- ═══ BOTTOM ACCENT ═══ -->
<tr><td style=""height:4px;background:linear-gradient(90deg,#8b5cf6 0%,#22d3ee 50%,#3b82f6 100%);""></td></tr>

</table>
<!-- /Main card -->

<!-- Sub-footer text -->
<table role=""presentation"" cellpadding=""0"" cellspacing=""0"" border=""0"" width=""600"" style=""max-width:600px;width:100%;margin-top:16px;"">
  <tr><td align=""center"" style=""font-size:11px;color:#475569;line-height:1.6;padding:0 16px;"">
    Ye email isliye aayi kyunki kisi ne tumhare portfolio contact form se message bheja hai.
  </td></tr>
</table>

</td></tr>
</table>

</body>
</html>";
    }

    public async Task SendAutoReplyAsync(
        string recipientName,
        string recipientEmail,
        string originalSubject,
        CancellationToken cancellationToken = default)
    {
        if (!_settings.Enabled)
        {
            _logger.LogInformation("Email notifications disabled");
            return;
        }

        try
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(_settings.SenderName, _settings.SenderEmail));
            email.To.Add(new MailboxAddress(recipientName, recipientEmail));
            email.Subject = $"Re: {originalSubject} — Thank you for reaching out!";

            var firstName = string.IsNullOrWhiteSpace(recipientName)
                ? "there"
                : recipientName.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries)[0];

            var html = BuildAutoReplyTemplate(firstName);

            email.Body = new TextPart("html") { Text = html };

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_settings.SmtpHost, _settings.SmtpPort, SecureSocketOptions.StartTls, cancellationToken);
            await smtp.AuthenticateAsync(_settings.SenderEmail, _settings.AppPassword, cancellationToken);
            await smtp.SendAsync(email, cancellationToken);
            await smtp.DisconnectAsync(true, cancellationToken);

            _logger.LogInformation("Auto-reply sent to {Recipient}", recipientEmail);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send auto-reply to {Recipient}", recipientEmail);
        }
    }

    private static string BuildAutoReplyTemplate(string firstName)
    {
        return $@"
<!DOCTYPE html>
<html lang=""en"">
<head>
<meta charset=""UTF-8"">
<meta name=""viewport"" content=""width=device-width,initial-scale=1.0"">
<title>Thank you</title>
</head>
<body style=""margin:0;padding:0;background-color:#050914;font-family:-apple-system,BlinkMacSystemFont,'Segoe UI',Roboto,'Helvetica Neue',Arial,sans-serif;-webkit-font-smoothing:antialiased;"">

<table role=""presentation"" cellpadding=""0"" cellspacing=""0"" border=""0"" width=""100%"" style=""background-color:#050914;padding:32px 16px;"">
<tr><td align=""center"">

<table role=""presentation"" cellpadding=""0"" cellspacing=""0"" border=""0"" width=""600"" style=""max-width:600px;width:100%;background-color:#0a1224;border-radius:20px;overflow:hidden;box-shadow:0 20px 60px rgba(0,0,0,0.5),0 0 0 1px rgba(59,130,246,0.15);"">

<!-- Top accent -->
<tr><td style=""height:4px;background:linear-gradient(90deg,#3b82f6 0%,#22d3ee 50%,#8b5cf6 100%);""></td></tr>

<!-- Header -->
<tr><td style=""background:linear-gradient(135deg,#0f172a 0%,#050914 100%);padding:36px 36px 28px;"">

  <table role=""presentation"" width=""100%"" cellpadding=""0"" cellspacing=""0"">
    <tr>
      <td align=""left"" valign=""middle"">
        <table role=""presentation"" cellpadding=""0"" cellspacing=""0"">
          <tr>
            <td style=""width:44px;height:44px;background:linear-gradient(135deg,#3b82f6,#22d3ee);border-radius:12px;text-align:center;vertical-align:middle;font-weight:900;font-size:16px;color:#ffffff;letter-spacing:-0.5px;box-shadow:0 8px 24px rgba(59,130,246,0.4);"">
              SY
            </td>
            <td style=""padding-left:12px;vertical-align:middle;"">
              <div style=""font-size:14px;font-weight:700;color:#ffffff;letter-spacing:-0.3px;line-height:1.2;"">Subhash Yadav</div>
              <div style=""font-size:11px;color:#22d3ee;font-weight:600;letter-spacing:0.5px;margin-top:2px;"">.NET FULL STACK DEVELOPER</div>
            </td>
          </tr>
        </table>
      </td>
      <td align=""right"" valign=""middle"">
        <span style=""display:inline-block;padding:6px 14px;background:rgba(34,211,238,0.12);border:1px solid rgba(34,211,238,0.3);border-radius:999px;font-size:10px;font-weight:800;color:#22d3ee;letter-spacing:1.5px;text-transform:uppercase;"">
          ✓ RECEIVED
        </span>
      </td>
    </tr>
  </table>

  <div style=""margin-top:32px;"">
    <div style=""font-size:11px;font-weight:800;color:#3b82f6;letter-spacing:2.5px;text-transform:uppercase;margin-bottom:8px;"">MESSAGE CONFIRMED</div>
    <h1 style=""margin:0;font-size:32px;font-weight:900;color:#ffffff;letter-spacing:-1px;line-height:1.15;"">
      Thank you, <span style=""background:linear-gradient(135deg,#22d3ee,#3b82f6);-webkit-background-clip:text;-webkit-text-fill-color:transparent;background-clip:text;"">{firstName}</span>!
    </h1>
  </div>

</td></tr>

<!-- Body -->
<tr><td style=""padding:8px 36px 0;"">

  <!-- Intro -->
  <p style=""margin:0 0 24px;font-size:15px;line-height:1.75;color:#cbd5e1;font-weight:400;"">
    I've received your message and I truly appreciate you taking the time to reach out. Your message is important to me, and I'll get back to you as soon as possible.
  </p>

  <!-- Response time card -->
  <table role=""presentation"" width=""100%"" cellpadding=""0"" cellspacing=""0"" style=""background:linear-gradient(135deg,rgba(16,185,129,0.08) 0%,rgba(34,211,238,0.05) 100%);border:1px solid rgba(16,185,129,0.25);border-radius:14px;margin-bottom:24px;"">
    <tr><td style=""padding:18px 22px;"">
      <table role=""presentation"" width=""100%"" cellpadding=""0"" cellspacing=""0"">
        <tr>
          <td valign=""middle"" style=""width:40px;"">
            <div style=""width:40px;height:40px;border-radius:10px;background:rgba(16,185,129,0.15);border:1px solid rgba(16,185,129,0.3);text-align:center;line-height:40px;font-size:18px;"">
              ⏱
            </div>
          </td>
          <td valign=""middle"" style=""padding-left:14px;"">
            <div style=""font-size:10px;font-weight:800;color:#10b981;letter-spacing:1.5px;text-transform:uppercase;margin-bottom:3px;"">EXPECTED RESPONSE</div>
            <div style=""font-size:15px;color:#ffffff;font-weight:700;letter-spacing:-0.3px;"">Within 24 hours</div>
          </td>
        </tr>
      </table>
    </td></tr>
  </table>

  <!-- What happens next -->
  <div style=""font-size:10px;font-weight:800;color:#64748b;letter-spacing:2px;text-transform:uppercase;margin-bottom:14px;"">WHAT HAPPENS NEXT</div>

  <table role=""presentation"" width=""100%"" cellpadding=""0"" cellspacing=""0"" style=""margin-bottom:24px;"">
    <tr><td valign=""top"" style=""padding-bottom:14px;"">
      <table role=""presentation"" width=""100%"" cellpadding=""0"" cellspacing=""0"">
        <tr>
          <td valign=""top"" style=""width:32px;padding-top:2px;"">
            <div style=""width:24px;height:24px;border-radius:50%;background:linear-gradient(135deg,#3b82f6,#22d3ee);text-align:center;line-height:24px;font-size:12px;font-weight:900;color:#ffffff;"">1</div>
          </td>
          <td style=""padding-left:12px;font-size:14px;line-height:1.6;color:#cbd5e1;"">
            <strong style=""color:#ffffff;font-weight:700;"">I review your message</strong> — Every message gets my personal attention.
          </td>
        </tr>
      </table>
    </td></tr>
    <tr><td valign=""top"" style=""padding-bottom:14px;"">
      <table role=""presentation"" width=""100%"" cellpadding=""0"" cellspacing=""0"">
        <tr>
          <td valign=""top"" style=""width:32px;padding-top:2px;"">
            <div style=""width:24px;height:24px;border-radius:50%;background:linear-gradient(135deg,#3b82f6,#22d3ee);text-align:center;line-height:24px;font-size:12px;font-weight:900;color:#ffffff;"">2</div>
          </td>
          <td style=""padding-left:12px;font-size:14px;line-height:1.6;color:#cbd5e1;"">
            <strong style=""color:#ffffff;font-weight:700;"">I reply personally</strong> — Expect a thoughtful response from me directly.
          </td>
        </tr>
      </table>
    </td></tr>
    <tr><td valign=""top"">
      <table role=""presentation"" width=""100%"" cellpadding=""0"" cellspacing=""0"">
        <tr>
          <td valign=""top"" style=""width:32px;padding-top:2px;"">
            <div style=""width:24px;height:24px;border-radius:50%;background:linear-gradient(135deg,#3b82f6,#22d3ee);text-align:center;line-height:24px;font-size:12px;font-weight:900;color:#ffffff;"">3</div>
          </td>
          <td style=""padding-left:12px;font-size:14px;line-height:1.6;color:#cbd5e1;"">
            <strong style=""color:#ffffff;font-weight:700;"">We make it happen</strong> — If there's a fit, we'll discuss next steps.
          </td>
        </tr>
      </table>
    </td></tr>
  </table>

  <!-- Divider -->
  <div style=""height:1px;background:linear-gradient(90deg,transparent,rgba(59,130,246,0.3),transparent);margin:8px 0 24px;""></div>

  <!-- Signature -->
  <div style=""font-size:15px;line-height:1.7;color:#cbd5e1;font-weight:400;margin-bottom:8px;"">
    Warm regards,
  </div>
  <div style=""font-size:17px;font-weight:800;color:#ffffff;letter-spacing:-0.4px;margin-bottom:4px;"">
    Subhash Yadav
  </div>
  <div style=""font-size:12px;color:#22d3ee;font-weight:600;letter-spacing:0.5px;"">
    .NET Full Stack Developer
  </div>

</td></tr>

<!-- Social connect -->
<tr><td style=""padding:28px 36px 0;"">
  <table role=""presentation"" width=""100%"" cellpadding=""0"" cellspacing=""0"" style=""background:rgba(15,23,42,0.6);border:1px solid rgba(148,163,184,0.1);border-radius:14px;"">
    <tr><td align=""center"" style=""padding:18px 20px;"">
      <div style=""font-size:10px;font-weight:800;color:#64748b;letter-spacing:2px;text-transform:uppercase;margin-bottom:12px;"">CONNECT WITH ME</div>
      <div style=""font-size:13px;color:#cbd5e1;line-height:1.7;"">
        <a href=""https://github.com/subhash-dotnet-dev"" style=""color:#22d3ee;text-decoration:none;font-weight:600;"">GitHub</a>
        &nbsp;·&nbsp;
        <a href=""https://www.linkedin.com/in/subhash-dotnet-dev/"" style=""color:#22d3ee;text-decoration:none;font-weight:600;"">LinkedIn</a>
        &nbsp;·&nbsp;
        <a href=""https://leetcode.com/subhashyadav"" style=""color:#22d3ee;text-decoration:none;font-weight:600;"">LeetCode</a>
      </div>
    </td></tr>
  </table>
</td></tr>

<!-- Footer -->
<tr><td style=""padding:32px 36px 32px;"">
  <div style=""font-size:11px;color:#475569;line-height:1.6;text-align:center;font-weight:500;"">
    This is an automated confirmation — no reply needed.<br>
    Your message is already in my inbox. 💙
  </div>
</td></tr>

<!-- Bottom accent -->
<tr><td style=""height:4px;background:linear-gradient(90deg,#8b5cf6 0%,#22d3ee 50%,#3b82f6 100%);""></td></tr>

</table>

</td></tr>
</table>

</body>
</html>";
    }
}
