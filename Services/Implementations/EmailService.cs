using Backend.Services.Interface;
using Resend;

namespace Backend.Services.Implementations {
    public class EmailService : IEmailService {
        private readonly IResend _resend;
        private readonly IConfiguration _configuration;

        public EmailService(IResend resend, IConfiguration configuration) {
            _resend = resend;
            _configuration = configuration;
        }

        public async Task SendVerifyEmail(string email, string verifyToken) {
            var recipient = _devOverrideEmail ?? email;
            var verifyUrl = $"{_frontendUrl}/verify-email?token={verifyToken}";
            var devNote = _devOverrideEmail != null
                ? $"<p style=\"background:#fff3cd;padding:8px;border-radius:4px;font-size:13px;\">" +
                  $"[DEV] Email goc gui den: <strong>{email}</strong></p>"
                : "";

            var html = BuildEmailHtml(
                devNote: devNote,
                heading: "Xac thuc Email cua ban",
                body: "Cam on ban da dang ky tai khoan tai <strong>Jolibi</strong>.<br/>Nhan vao nut ben duoi de xac thuc dia chi email:",
                buttonUrl: verifyUrl,
                buttonText: "Xac thuc Email",
                note: "Link nay co hieu luc trong <strong>24 gio</strong>. Neu ban khong thuc hien dang ky, hay bo qua email nay."
            );
            var message = new EmailMessage {
                From = _fromEmail,
                Subject = "Xac thuc email dang ky - Jolibi",
                HtmlBody = html
            };
            message.To.Add(recipient);
            await _resend.EmailSendAsync(message);
        }

        public async Task SendChangePasswordEmail(string email, string resetToken) {
            var recipient = _devOverrideEmail ?? email;
            var resetUrl = $"{_frontendUrl}/reset-password?token={resetToken}";
            var devNote = _devOverrideEmail != null
                ? $"<p style=\"background:#fff3cd;padding:8px;border-radius:4px;font-size:13px;\">" +
                  $"[DEV] Email goc gui den: <strong>{email}</strong></p>"
                : "";

            var html = BuildEmailHtml(
                devNote: devNote,
                heading: "Dat lai mat khau",
                body: "Chung toi nhan duoc yeu cau dat lai mat khau cho tai khoan <strong>Jolibi</strong> cua ban.<br/>Nhan vao nut ben duoi de dat lai mat khau:",
                buttonUrl: resetUrl,
                buttonText: "Dat lai mat khau",
                note: "Link nay co hieu luc trong <strong>1 gio</strong>. Neu ban khong yeu cau dat lai mat khau, hay bo qua email nay."
            );
            var message = new EmailMessage {
                From = _fromEmail,
                Subject = "Dat lai mat khau - Jolibi",
                HtmlBody = html
            };
            message.To.Add(recipient);
            await _resend.EmailSendAsync(message);
        }

        private static string BuildEmailHtml(
            string devNote, string heading, string body,
            string buttonUrl, string buttonText, string note) {
            return
                "<!DOCTYPE html><html><head><meta charset=\"UTF-8\"/></head>" +
                "<body style=\"font-family:Arial,sans-serif;background:#f4f4f4;padding:20px;\">" +
                "<div style=\"max-width:600px;margin:auto;background:white;border-radius:8px;padding:32px;\">" +
                devNote +
                "<h2 style=\"color:#d97706;\">" + heading + "</h2>" +
                "<p>" + body + "</p>" +
                "<p><a href=\"" + buttonUrl + "\" style=\"display:inline-block;background:#d97706;color:white;" +
                "padding:12px 24px;border-radius:6px;text-decoration:none;font-weight:bold;margin:16px 0;\">" +
                buttonText + "</a></p>" +
                "<p style=\"color:#666;font-size:14px;\">" + note + "</p>" +
                "<hr style=\"border:none;border-top:1px solid #eee;margin:24px 0;\"/>" +
                "<p style=\"color:#999;font-size:12px;\">&copy; 2025 Jolibi. Tat ca quyen duoc bao luu.</p>" +
                "</div></body></html>";
        }
    }
}
