using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Ordering.Application.Contracts.Infrastructure;
using Ordering.Application.Models;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Ordering.Infrastructure.Mail; 

public class EmailService : IEmailService{
    public EmailSettings EmailSettings { get; }
    public ILogger Logger { get; }
    public EmailService(IOptions<EmailSettings> emailSettings, ILogger<EmailService> logger) {
        EmailSettings = emailSettings.Value;
        Logger = logger;
    }
    
    public async Task<bool> SendEmail(Email email) {
        var client = new SendGridClient(EmailSettings.ApiKey);
        var to = new EmailAddress(email.To);
        var from = new EmailAddress {
            Email = EmailSettings.FromAddress,
            Name = EmailSettings.FromName
        };
        
        var sendGridMessage = MailHelper.CreateSingleEmail(from, to, email.Subject, email.Body, email.Body);
        var response = await client.SendEmailAsync(sendGridMessage);
        
        Logger.LogInformation("Email sent.");
        
        if(response.StatusCode == System.Net.HttpStatusCode.Accepted || response.StatusCode == System.Net.HttpStatusCode.OK) {
            return true;
        }
        
        Logger.LogError("Email sending failed.");
        return false;
    }
}