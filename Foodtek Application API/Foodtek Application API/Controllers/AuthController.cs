using Foodtek_Application_API.DTOs.LogIn.Request;
using Foodtek_Application_API.DTOs.LogIn.Response;
using Foodtek_Application_API.DTOs.RestPassword.Request;
using Foodtek_Application_API.DTOs.SignUp;
using Foodtek_Application_API.DTOs.Verification.Request;
using Foodtek_Application_API.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Foodtek_Application_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthanication _authentication;

        public AuthController(IAuthanication authenticationService)
        {
            _authentication = authenticationService;
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp([FromBody] SignUpResponse input)
        {
            try
            {
                var response = await _authentication.SignUp(input);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.InnerException?.Message ?? ex.Message);
            }

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest input)
        {
            try
            {
                var response = await _authentication.Login(input);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
             
            }


        }
       

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest input)
        {
            try

            {
                var response = await _authentication.ResetUserPassword(input);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("SendOTP")]
        public async Task<IActionResult> SendOTP([FromBody] string email)
        {

            try
            {
                var response = await _authentication.SendOTP(email);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);

            }
        }


        [HttpPost("VerifyOTP")]
        public async Task<IActionResult> Verification(VerificationRequest input)
        {
            try
            {
                var response = await _authentication.Verification(input);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);

            }


        }

        [HttpPost("SignOut")]
        public async Task<IActionResult> SignOut(int userId)
        {
            try
            {
                var response = await _authentication.SignOut(userId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
