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
        public AuthController(IEmployeeService employeeService, IOptions<JwtSettings> jwtSettings, IOptions<EmailSettings> emailSettings, WBLMSDbContext dbContext, AuthService authService)
        {
            _employeeService = employeeService;
            _jwtSettings = jwtSettings.Value;
            _emailSettings = emailSettings.Value;
            _dbContext = dbContext;
            _authService = authService;
        }

        [HttpGet("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] LoginDTO loginDTO)
        {
            if (loginDTO == null)
                return BadRequest(new
                {
                    StatusCode = 400,
                    ErrorMessage = "Invalid Login Details!"
                });
            var employee = await _employeeService.GetEmployeeByEmailAsync(loginDTO.Email);
            if (employee == null)
            {
                return NotFound(new 
                {   StatusCode = 404,
                    Message = "User Not Found!" 
                });
            }

            if (!PasswordHashing.Verify(loginDTO.Password, employee.Password))
            {
                return BadRequest(new {StatusCode = 400, Message = "Password is Incorrect!" });
            }
            employee.Token.AccessToken = _authService.CreateJwt(employee);
            var newAccessToken = employee.Token.AccessToken;
            var newRefreshToken = _authService.CreateRefreshToken();
            employee.Token.RefreshToken = newRefreshToken;
            //employee.Token.RefreshTokenExpiry = DateTime.Now.AddDays(5);

            return Ok(new TokenAPIDTO()
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            });
        }

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
