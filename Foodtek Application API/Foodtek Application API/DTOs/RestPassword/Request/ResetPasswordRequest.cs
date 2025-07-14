namespace Foodtek_Application_API.DTOs.RestPassword.Request
{
    public class ResetPasswordRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
        public string OTPCode { get; set; }
    }
}
