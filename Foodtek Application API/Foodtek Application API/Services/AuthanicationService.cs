using System;
using Foodtek_Application_API.DTOs.LogIn.Request;
using Foodtek_Application_API.DTOs.LogIn.Response;
using Foodtek_Application_API.DTOs.RestPassword.Request;
using Foodtek_Application_API.DTOs.SignUp;
using Foodtek_Application_API.DTOs.Verification.Request;
using Foodtek_Application_API.Helper;
using Foodtek_Application_API.Interface;
using Foodtek_Application_API.Models;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Mail;
using static Foodtek_Application_API.Helper.Enums.StatusEnum;

namespace Foodtek_Application_API.Services
{
    public class AuthanicationService : IAuthanication
    {
        private readonly FoodtekDbContext context;

        public AuthanicationService(FoodtekDbContext _context)
        {
            context = _context;

        }

        public async Task<string> SignUp(SignUpResponse input)
        {
            if (!ValidationHelper.ISValidFullName(input.FullName) ||
                !ValidationHelper.IsValidEmail(input.Email) ||
                !ValidationHelper.IsValidBrithOfDate(input.BirthOfDate) ||
                !ValidationHelper.IsValidPhoneNumber(input.PhoneNumber) ||
                !ValidationHelper.IsValidPassword(input.Password))
            {

                return "Invalid input data.";
            }


            if (context.Users.Any(u => u.Email == input.Email))
            {
                return "Email already exists.";
            }

            User user = new User
            {
                FullName = input.FullName,
                Email = input.Email,
                Password = HashingHelper.HashValueWith384(input.Password),
                PhoneNumber = input.PhoneNumber,
                BirthOfDate = input.BirthOfDate,
                Status = Status.Active.ToString(),
                CreatedBy = "System",
                UpdatedBy = "System",
                CreationDate = DateTime.Now,
                JoinDate = DateTime.Now,
                IsVerified = false,
                IsActive = true,
                RoleId = 2,
                Otpcode = new Random().Next(11111, 99999).ToString(),
                Otpexpiry = DateTime.Now.AddMinutes(5)
            };
            await MailingHelper.SendEmail(input.Email, user.Otpcode, "Sign Up  OTP", "Complete Sign Up Operation");

            context.Users.Add(user);

            await context.SaveChangesAsync();

            return "Please verify your email using the OTP sent.";
        }

    
        
        public async Task<string> Login(LoginRequest input)
        {

            if (string.IsNullOrWhiteSpace(input.Email) || string.IsNullOrWhiteSpace(input.Password))
                return ("Email and Password are required");

            var user = context.Users.Where(u => u.Email == input.Email && u.Password == input.Password && u.IsLogedIn == false).SingleOrDefault();

            if (user == null)
            {
                return "User not found";
            }
            Random random = new Random();
            var OTP = random.Next(11111, 99999);
            user.Otpcode = OTP.ToString();
            user.Otpexpiry = DateTime.Now.AddMinutes(5);
            await MailingHelper.SendEmail(input.Email, user.Otpcode, "Sign In  OTP", "Complete Sign in Operation");

            context.Update(user);
            context.SaveChanges();

            return "Check your email OTP has been sent!";


        }
        public async Task<bool> ResetUserPassword(ResetPasswordRequest input)
        {

            if (input == null || string.IsNullOrWhiteSpace(input.Email)
           || string.IsNullOrWhiteSpace(input.Password)
           || string.IsNullOrWhiteSpace(input.ConfirmPassword)
           || string.IsNullOrWhiteSpace(input.OTPCode))

            {
                return false;

            }

            if (!ValidationHelper.IsValidEmail(input.Email))
            {
                return false;
            }

            var user = context.Users.Where(u => u.Email == input.Email && u.Otpcode == input.OTPCode
            && u.IsLogedIn == false && u.Otpexpiry > DateTime.Now).SingleOrDefault();

            if (user == null)
            {
                return false;
            }
            if (input.Password != input.ConfirmPassword)
            {
                return false;
            }
            user.Password = input.ConfirmPassword;
            user.Otpcode = null;
            user.Otpexpiry = null;

            context.Users.Update(user);
            await context.SaveChangesAsync();

            return true;
        }


        public async Task<bool> SendOTP(string email)
        {

            if (!ValidationHelper.IsValidEmail(email))
                return false;

            var user = await context.Users.FirstOrDefaultAsync(u => u.Email == email && u.IsLogedIn == false);

            if (user == null)
            {
                return false;
            }
            Random otp = new Random();
            user.Otpcode = otp.Next(11111, 99999).ToString();
            user.Otpexpiry = DateTime.Now.AddMinutes(3);
            await MailingHelper.SendEmail(email, user.Otpcode, "Reset Password OTP", "Complete Reset Password");


            context.Update(user);
            await context.SaveChangesAsync();

            return true;
        }



        public async Task<string> Verification(VerificationRequest input)
        {

            if (string.IsNullOrWhiteSpace(input.Email) || string.IsNullOrWhiteSpace(input.OTPCode))
                return ("Email and OTP code are required.");

            var user = context.Users.Where(u => u.Email == input.Email && u.Otpcode == input.OTPCode
            && u.IsLogedIn == false && u.Otpexpiry > DateTime.Now).SingleOrDefault();

            if (user == null)
            {
                return "User not found";
            }
            if (input.IsSignup)
            {

                user.IsVerified = true;
                user.Otpexpiry = null;
                user.Otpcode = null;
                context.Update(user);
                context.SaveChanges();
                return "Your Account Is Verifyed";
            }
            else
            {
                user.LastLoginTime = DateTime.Now;
                user.IsLogedIn = true;
                user.Otpexpiry = null;
                user.Otpcode = null;

                context.Update(user);
                context.SaveChanges();
                var response = TokenHelper.GenerateJWTToken(user.Id.ToString(), "Client");
                return response;

            }
        }
         public async Task<bool> SignOut(int userId)
        {
            var user = context.Users.SingleOrDefault(u => u.Id == userId);
            if (user == null)
            {
                return false;
            }

            user.LastLoginTime = DateTime.Now;
            user.IsLogedIn = false;

            context.Update(user);
            context.SaveChanges();

            return true;
        }

    }

    }
