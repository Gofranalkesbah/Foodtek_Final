namespace Foodtek_Application_API.DTOs.Verification.Request
{
    public class VerificationRequest
    {
        public string Email { get; set; }

        public string OTPCode { get; set; }

        public bool IsSignup { get; set; }
    }
}
