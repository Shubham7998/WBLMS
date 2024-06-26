﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Security.Cryptography;
using WBLMS.Data;
using WBLMS.DTO;
using WBLMS.IRepositories;
using WBLMS.IServices;
using WBLMS.Models;
using WBLMS.Services;
using WBLMS.Utilities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WBLMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly JwtSettings _jwtSettings;
        private readonly EmailSettings _emailSettings;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly WBLMSDbContext _dbContext;
        private readonly AuthService _authService;
        private readonly IEmailService _emailService;

        public AuthController(IEmployeeService employeeService, IOptions<JwtSettings> jwtSettings, IOptions<EmailSettings> emailSettings, WBLMSDbContext dbContext, AuthService authService, IEmployeeRepository employeeRepository, IEmailService emailService)
        {
            _employeeService = employeeService;
            _jwtSettings = jwtSettings.Value;
            _emailSettings = emailSettings.Value;
            _dbContext = dbContext;
            _authService = authService;
            _employeeRepository = employeeRepository;
            _emailService = emailService;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] LoginDTO loginDTO)
        {
            try
            {
                if (loginDTO == null)
                    return BadRequest(new APIResponseDTO<EmptyResult>(400, null, "Invalid Login Details!"));
                //{
                //    StatusCode = 400,
                //    ErrorMessage = "Invalid Login Details!"
                //});
                var employee = await _employeeService.GetEmployeeByEmailAsync(loginDTO.Email);
                if (employee == null)
                {
                    return BadRequest(new APIResponseDTO<EmptyResult>(400, null, "User Not Found!"));
                    //{   StatusCode = 404,
                    //    Message = "User Not Found!" 
                    //});
                }

                if (!PasswordHashing.Verify(loginDTO.Password, employee.Password))
                {
                    return BadRequest(
                        new APIResponseDTO<EmptyResult>(400, null, "Password is Incorrect!")
                        /*new {StatusCode = 400, Message = "Password is Incorrect!" }*/
                        );
                }
                // Saving Tokens to DB then assigning to the employee
                var newAccessToken = _authService.CreateJwt(employee);
                var newRefreshToken = _authService.CreateRefreshToken();
                //var token = new Token()
                //{
                //    //Id = (long)employee.TokenId,
                //    AccessToken = newAccessToken,
                //    RefreshToken = newRefreshToken,
                //    EmployeeId = employee.Id,
                //    RefreshTokenExpiry = DateTime.Now.AddDays(5),
                //    PasswordResetExpiry = DateTime.Now.AddDays(5),
                //    PasswordResetToken = "random"
                //};
                //employee.Token.AccessToken = newAccessToken;
                //employee.Token.RefreshToken = newRefreshToken;
                if (employee.TokenId == null)
                {
                    var token = new Token()
                    {
                        //Id = (long)employee.TokenId,
                        AccessToken = newAccessToken,
                        RefreshToken = newRefreshToken,
                        EmployeeId = employee.Id,
                        RefreshTokenExpiry = DateTime.Now.AddDays(5),
                        PasswordResetExpiry = DateTime.Now.AddMinutes(5),
                        PasswordResetToken = "random"
                    };
                    var tokenIdentity = await _dbContext.Tokens.AddAsync(token);
                    await _dbContext.SaveChangesAsync();

                    employee.TokenId = tokenIdentity.Entity.Id;

                    _dbContext.Employees.Update(employee);
                    await _dbContext.SaveChangesAsync();
                }
                else
                {
                    var token = new Token()
                    {
                        Id = (long)employee.TokenId,
                        AccessToken = newAccessToken,
                        RefreshToken = newRefreshToken,
                        EmployeeId = employee.Id,
                        RefreshTokenExpiry = DateTime.Now.AddDays(5),
                        PasswordResetExpiry = DateTime.Now.AddMinutes(5),
                        PasswordResetToken = "random"
                    };
                    employee.Token.AccessToken = newAccessToken;
                    employee.Token.RefreshToken = newRefreshToken;
                    _dbContext.Employees.Update(employee);
                    await _dbContext.SaveChangesAsync();
                }

                return Ok(new TokenAPIDTO()
                {
                    AccessToken = newAccessToken,
                    RefreshToken = newRefreshToken
                });
            }
            catch (Exception ex)
            {
                return NotFound(new APIResponseDTO<EmptyResult>(500, null, ex.Message));
            }
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] TokenAPIDTO tokenApiDTO)
        {
            try
            {
                if (tokenApiDTO == null)
                {
                    return BadRequest(new
                    {
                        StatusCode = 400,
                        Message = "Invalid Client Request"
                    });
                }
                string accessToken = tokenApiDTO.AccessToken;
                string refreshToken = tokenApiDTO.RefreshToken;
                var principal = _authService.GetPrincipalFromExpiredToken(accessToken);

                var email = "";
                foreach (var identity in principal.Identities)
                {
                    var emailClaim = identity.FindFirst(ClaimTypes.Email);
                    if (emailClaim != null)
                    {
                        email = emailClaim.Value;
                        break;
                    }
                }
                var employee = await _employeeService.GetEmployeeByEmailAsync(email);
                if (employee is null || employee.Token.RefreshToken != refreshToken || employee.Token.RefreshTokenExpiry <= DateTime.Now)
                {
                    return BadRequest(new
                    {
                        StatusCode = 400,
                        ErrorMessage = "Invalid Request Or Refresh Token is expired!"
                    });
                }
                var newAccessToken = _authService.CreateJwt(employee);
                var newRefreshToken = _authService.CreateRefreshToken();
                employee.Token.RefreshToken = newRefreshToken;
                employee.Token.AccessToken = newAccessToken;
                await _dbContext.SaveChangesAsync();
                return Ok(new TokenAPIDTO()
                {
                    AccessToken = newAccessToken,
                    RefreshToken = newRefreshToken
                });
            }
            catch (Exception ex)
            {
                return NotFound(new APIResponseDTO<EmptyResult>(500, null, ex.Message));
            }
        }


        [HttpPost("send-forget-email/{email}")]

        public async Task<IActionResult> SendEmail(string email)
        {
            try
            {
                var getEmployee = await _employeeService.GetEmployeeByEmailAsync(email);
                if (getEmployee == null)
                {
                    //return NotFound(new
                    //{
                    //    StatusCode = 404,
                    //    Message = "email doesnt exist"
                    //});
                    return NotFound(new APIResponseDTO<EmptyResult>(500, null, "email doesnt exist"));

                }

                var token = await _employeeRepository.GetTokenAsync(getEmployee.Id);
                if (token == null)
                {
                    return NotFound(new APIResponseDTO<EmptyResult>(500, null, "You have to login first"));
                }

                //var token = getEmployee.Token;

                var tokenBytes = RandomNumberGenerator.GetBytes(64);
                var emailToken = Convert.ToBase64String(tokenBytes);
                token.PasswordResetToken = emailToken;
                token.PasswordResetExpiry = DateTime.Now.AddMinutes(15);
                string from = _emailSettings.From;
                var emailModel = new EmailModel(email, "Reset Password", EmailBody.EmailStringBody(email, emailToken));

                _emailService.SendEmail(emailModel);

                _dbContext.Entry(getEmployee).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();

                //return Ok(new
                //{
                //    StatusCode = 200,
                //    Message = "Email sent"
                //});
                return Ok(new APIResponseDTO<EmptyResult>(200, null, "Email sent"));
            }
            catch (Exception ex)
            {
                return NotFound(new APIResponseDTO<EmptyResult>(500, null, ex.Message));
            }
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDTO resetPasswordDTO)
        {
            try
            {
                var newToken = resetPasswordDTO.EmailToken.Replace(" ", "+");

                var employee = await _employeeService.GetEmployeeByEmailAsync(resetPasswordDTO.Email);

                if (employee == null)
                {
                    return NotFound(new
                    {
                        StatusCode = 404,
                        Message = "email doesnt exist"
                    });
                }
                var token = await _employeeService.GetTokenByEmployeeIdAsync(employee.Id);



                //var isOldPasswordCorrect = PasswordHashing.Verify(resetPasswordDTO.OldPassword, employee.Password);
                //if (!isOldPasswordCorrect)
                //{

                //    return NotFound(new APIResponseDTO<EmptyResult>(204, null, "Old password you have enter is wrong"));

                //}


                var tokenCode = token.PasswordResetToken;
                DateTime emailTokenExpiry = token.PasswordResetExpiry;

                var isTokenValid = tokenCode != resetPasswordDTO.EmailToken;

                if (isTokenValid || emailTokenExpiry < DateTime.Now)
                {
                    return BadRequest(new
                    {
                        StatusCode = 400,
                        Message = "Invalid reset link"
                    });
                }
                token.PasswordResetToken = "random";

                employee.Password = PasswordHashing.getHashPassword(resetPasswordDTO.NewPassword);
                _dbContext.Entry(token).State = EntityState.Modified;
                _dbContext.Entry(employee).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
                //return Ok(new
                //{
                //    StatusCode = 200,
                //    Message = "Password reset successfully"
                //});
                return Ok(new APIResponseDTO<EmptyResult>(200, null, "Password reset successfully"));

            }
            catch (Exception ex)
            {
                return NotFound(new APIResponseDTO<EmptyResult>(500, null, ex.Message));
            }
        }
        [HttpPost("forget-password")]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordDTO forgetPasswordDTO)
        {
            var newToken = forgetPasswordDTO.EmailToken.Replace(" ", "+");

            var employee = await _employeeService.GetEmployeeByEmailAsync(forgetPasswordDTO.Email);

            if (employee == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "email doesnt exist"
                });
            }
            var token = await _employeeService.GetTokenByEmployeeIdAsync(employee.Id);

            var tokenCode = token.PasswordResetToken;
            DateTime emailTokenExpiry = token.PasswordResetExpiry;

            var isTokenValid = tokenCode == forgetPasswordDTO.EmailToken;

            if (!isTokenValid || emailTokenExpiry < DateTime.Now)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = "Invalid reset link"
                });
            }
            token.PasswordResetToken = "random";

            employee.Password = PasswordHashing.getHashPassword(forgetPasswordDTO.NewPassword);
            _dbContext.Entry(token).State = EntityState.Modified;
            _dbContext.Entry(employee).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            //return Ok(new
            //{
            //    StatusCode = 200,
            //    Message = "Password reset successfully"
            //});
            return Ok(new APIResponseDTO<EmptyResult>(200, null, " New password set successfully"));
        }                
    }
}

