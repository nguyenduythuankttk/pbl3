using Backend.Services.Interface;
using Resend;
namespace Backend.Services.Implementations {
    public class EmailService : IEmailService {
        private readonly IResend _resend;
        private readonly string _fromEmail;

        public EmailService(IResend resend, IConfiguration configuration) {
            _resend = resend;
            _fromEmail = configuration["Resend:FromEmail"] ?? throw new Exception("Resend:FromEmail is not configured");
        }
        

    }
}
