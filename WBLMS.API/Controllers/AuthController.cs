using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using WBLMS.Data;
using WBLMS.DTO;
using WBLMS.IRepositories;
using WBLMS.IServices;
using WBLMS.Models;
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
        public AuthController(IEmployeeService employeeService, IOptions<JwtSettings> jwtSettings, IOptions<EmailSettings> emailSettings, WBLMSDbContext dbContext)
        {
            _employeeService = employeeService;
            _jwtSettings = jwtSettings.Value;
            _emailSettings = emailSettings.Value;
            _dbContext = dbContext;
        }

        //[HttpGet("authenticate")]
        //public async Task<IActionResult> Authenticate([FromBody] User userObj)
        //{
        //    if (userObj == null)
        //        return BadRequest();
        //    var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == userObj.Username);
        //    if (user == null)
        //    {
        //        return NotFound(new { Message = "User Not Found!" });
        //    }

        //    if (!PasswordHasher.VerifyPassword(userObj.Password, user.Password))
        //    {
        //        return BadRequest(new { Message = "Password is Incorrect" });
        //    }
        //    user.Token = CreateJwt(user);
        //    var newAccessToken = user.Token;
        //    var newRefreshToken = CreateRefreshToken();
        //    user.RefreshToken = newRefreshToken;
        //    user.RefreshTokenExpiryTime = DateTime.Now.AddDays(5);
        //    await _context.SaveChangesAsync();

        //    return Ok(new TokenApiDTO()
        //    {
        //        AccessToken = newAccessToken,
        //        RefreshToken = newRefreshToken
        //    });
        //}

        [HttpPost("send-reset-email/{email}")]

        public async Task<IActionResult> SendEmail(string email)
        {
            var getEmployee = await _employeeService.GetEmployeeByEmailAsync(email);
            if (getEmployee == null) 
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "email doesnt exist"
                });
            }

            var token = await _employeeRepository.GetTokenAsync(getEmployee.Id == null ? 1 : getEmployee.Id);

            var tokenBytes = RandomNumberGenerator.GetBytes(64);
            var emailToken = Convert.ToBase64String(tokenBytes);
            token.PasswordResetToken = emailToken;
            token.PasswordResetExpiry = DateOnly.FromDateTime(DateTime.Now.AddMinutes(15));
            string from = _emailSettings.From;
            var emailModel = new EmailModel(email, "Reset Password", EmailBody.EmailStringBody(email, emailToken));

            _dbContext.Entry(getEmployee).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

            return Ok(new
            {
                StatusCode = 200,
                Message = "Email sent"
            });
        }
    }
}
