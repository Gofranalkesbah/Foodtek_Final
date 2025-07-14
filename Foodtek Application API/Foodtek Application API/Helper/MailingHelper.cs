using SendGrid.Helpers.Mail;
using SendGrid;

namespace Foodtek_Application_API.Helper
{
    public static class MailingHelper
    {
        public static async Task SendEmail(string email, string code, string title, string message)
        {
            var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            if (string.IsNullOrWhiteSpace(apiKey))
                throw new InvalidOperationException("SENDGRID_API_KEY is not configured.");


            /* var options = new SendGridClientOptions
        {
            ApiKey = apiKey
        };
        options.SetDataResidency("eu"); 
        var client = new SendGridClient(options); */
            // uncomment the above 6 lines if you are sending mail using a regional EU subuser
            // and remove the client declaration just below
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("gofranalkesbah@gmail.com", "Foodtek Application API");
            var subject = title;
            var to = new EmailAddress(email, "User");
            var plainTextContent = $"Dear User {message}  Please Use The Following OTP Code {code} It Will be Expired With in 10 minutes";
            var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, "");
            var response = await client.SendEmailAsync(msg);
        }

        
    }
}
       
