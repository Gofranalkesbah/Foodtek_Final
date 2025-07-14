using Foodtek_Application_API.DTOs.LogIn.Request;
using Foodtek_Application_API.DTOs.RestPassword.Request;
using Foodtek_Application_API.DTOs.SignUp;
using Foodtek_Application_API.DTOs.Verification.Request;

namespace Foodtek_Application_API.Interface
{
    public interface IAuthanication
    {
        Task<string> SignUp(SignUpResponse input);
        Task<string> Login(LoginRequest input);
        Task<string> Verification(VerificationRequest input);
        Task<bool> SendOTP(string email);
        Task<bool> ResetUserPassword(ResetPasswordRequest input);

        Task<bool> SignOut(int userId);


    }
}
