using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBLMS.Data;
using WBLMS.Data.Migrations;
using WBLMS.DTO;
using WBLMS.IRepositories;
using WBLMS.IServices;
using WBLMS.Models;
using WBLMS.Utilities;

namespace WBLMS.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly WBLMSDbContext _dbContext;


        public EmployeeService(IEmployeeRepository employeeRepository, WBLMSDbContext dbContext)
        {
            _employeeRepository = employeeRepository;
            _dbContext = dbContext;
        }

        public async Task<GetEmployeeDTO> CreateEmployeeAsync(CreateEmployeeDTO employeeDTO)
        {
            var Password = PasswordHashing.getHashPassword(employeeDTO.Password);

            var employee = await _employeeRepository.CreateAsync
                (
                    new Employee()
                    {
                        FirstName = employeeDTO.FirstName,
                        LastName = employeeDTO.LastName,
                        Password = Password,
                        EmailAddress = employeeDTO.EmailAddress,
                        ContactNumber = employeeDTO.ContactNumber,
                        GenderId = employeeDTO.GenderId,
                        RoleId = employeeDTO.RoleId,
                        ManagerId = employeeDTO.ManagerId,
                        CreatedById = 1,
                        JoiningDate = DateOnly.FromDateTime(DateTime.Now),
                    }
                );
            // var newAccessToken = _authService.CreateJwt(employee);
            //var newRefreshToken = _authService.CreateRefreshToken();
            //var token = new Token()
            //{
            //    EmployeeId = employee.Id,
            //    AccessToken = "AccessToken",
            //    RefreshToken = "RefreshToken",
            //    RefreshTokenExpiry = DateTime.Now.AddDays(5),
            //    PasswordResetExpiry = DateTime.Now.AddDays(5),
            //    PasswordResetToken = "random"
            //};
            //var tokenData = await _dbContext.Tokens.AddAsync(token);

            var leaveBalance = new LeaveBalance()
            {
                Id = 0,
                EmployeeId = employee.Id,
                Balance = 25,
                TotalLeaves = 25
            };
            await _dbContext.LeaveBalances.AddAsync(leaveBalance);
            await _dbContext.SaveChangesAsync();


            if (employee != null)
            {
                //employee.TokenId = tokenData.Entity.Id;
                //await _dbContext.SaveChangesAsync();
            }

            return new GetEmployeeDTO
                            (
                                employee.Id,
                                employee.FirstName,
                                employee.LastName,
                                //employee.Password,
                                employee.EmailAddress,
                                employee.ContactNumber,
                                (long)employee.GenderId,
                                (long)employee.RoleId,
                                employee.ManagerId == null ? 1 : (long)employee.ManagerId,
                                employee.CreatedById == null ? 1 : (long)employee.CreatedById,
                                employee.JoiningDate
                            );

        }


        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            try
            {
                var employee = await _employeeRepository.GetAsyncById(id);
                if (employee != null)
                {
                    await _employeeRepository.DeleteAsync(employee);
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return false;
        }

        public async Task<Employee> GetEmployeeByEmailAsync(string email)
        {
            try
            {
                var result = await _employeeRepository.GetEmployeeByEmail(email);
                if (result != null)
                {
                    return result;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Token> GetTokenByEmployeeIdAsync(long id)
        {
            try
            {
                var token = await _employeeRepository.GetTokenAsync(id);
                return token;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<(IEnumerable<GetEmployeeDTO>, int)> GetAllEmployeeAsync(int page, int pageSize, string? sortColumn, string? sortOrder, GetEmployeeDTO employeeDTO)
        {
            try
            {
                var employeeObj = new Employee()
                {
                    FirstName = employeeDTO.FirstName,
                    LastName = employeeDTO.LastName,
                    //  Password = employeeDTO.Password,
                    EmailAddress = employeeDTO.EmailAddress,
                    ContactNumber = employeeDTO.ContactNumber,
                    GenderId = employeeDTO.GenderId,
                    RoleId = employeeDTO.RoleId,
                    ManagerId = employeeDTO.ManagerId,
                    CreatedById = employeeDTO.CreatedById,
                    JoiningDate = DateOnly.FromDateTime(DateTime.Now),
                };
                var employeeList = await _employeeRepository.GetAllEmployee(page, pageSize, sortColumn, sortOrder, employeeObj);

                var list = employeeList.Item1;
                var result = list.Select
                    (
                    employee =>
                        new GetEmployeeDTO
                            (
                                employee.Id,
                                employee.FirstName,
                                employee.LastName,
                                // employee.Password,
                                employee.EmailAddress,
                                employee.ContactNumber,
                                (long)employee.GenderId,
                                (long)employee.RoleId,
                                employee.ManagerId == null ? 1 : (long)employee.ManagerId,
                                employee.CreatedById == null ? 1 : (long)employee.CreatedById,
                                employee.UpdatedDate
                            )
                    );
                return (result, employeeList.Item2);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<GetEmployeeDTO> GetEmployeeByIdAsync(int id)
        {
            try
            {
                var employee = await _employeeRepository.GetAsyncById(id);
                if (employee != null)
                {
                    return new GetEmployeeDTO
                    (
                        employee.Id,
                                employee.FirstName,
                                employee.LastName,
                                // employee.Password,
                                employee.EmailAddress,
                                employee.ContactNumber,
                                (long)employee.GenderId,
                                (long)employee.RoleId,
                                employee.ManagerId == null ? 1 : (long)employee.ManagerId,
                                employee.CreatedById == null ? 1 : (long)employee.CreatedById,
                                employee.UpdatedDate
                    );
                }

            }
            catch (Exception e)
            {
                throw;
            }
            return null;
        }

        public async Task<GetEmployeeDTO> UpdateEmployeeAsync(UpdateEmployeeDTO employeeDTO, int id)
        {
            try
            {
                var oldEmployee = await _employeeRepository.GetAsyncById(id);

                if (oldEmployee != null)
                {
                    var hashPassword = PasswordHashing.getHashPassword(employeeDTO.Password);

                    oldEmployee.FirstName = employeeDTO.FirstName;
                    oldEmployee.LastName = employeeDTO.LastName;
                    oldEmployee.Password = hashPassword;
                    oldEmployee.EmailAddress = employeeDTO.EmailAddress;
                    oldEmployee.ContactNumber = employeeDTO.ContactNumber;
                    oldEmployee.GenderId = employeeDTO.GenderId;
                    oldEmployee.RoleId = employeeDTO.RoleId;
                    oldEmployee.ManagerId = employeeDTO.ManagerId;
                    oldEmployee.UpdateById = employeeDTO.UpdatedById;
                    oldEmployee.UpdatedDate = DateOnly.FromDateTime(DateTime.Now);

                    var employee = await _employeeRepository.UpdateAsync(oldEmployee);

                    return new GetEmployeeDTO
                    (
                                employee.Id,
                                employee.FirstName,
                                employee.LastName,
                                // employee.Password,
                                employee.EmailAddress,
                                employee.ContactNumber,
                                (long)employee.GenderId,
                                (long)employee.RoleId,
                                employee.ManagerId == null ? 1 : (long)employee.ManagerId,
                                employee.CreatedById == null ? 1 : (long)employee.CreatedById,
                                employee.UpdatedDate
                    );
                }
            }
            catch (Exception e) { throw; }
            return null;
        }

        public async Task<IEnumerable<Gender>> GetAllGenderAsync()
        {
            try
            {
                var genders = await _dbContext.Genders.ToListAsync();
                return genders;
            }
            catch (Exception e) { throw; }
        }

        public async Task<IEnumerable<Roles>> GetAllRolesAsync()
        {
            try
            {
                var roles = await _dbContext.Roles.ToListAsync();
                return roles;
            }
            catch (Exception e) { throw; }
        }

        public async Task<IEnumerable<GetManagerDTO>> GetAllManagersAsync(long id)
        {
            try
            {
                var employees = await _dbContext.Employees.ToListAsync();
                var managers = employees.FindAll
                    (
                        employee => employee.RoleId == id - 1
                    );
                var managerList = managers.Select
                    (
                        manager =>
                        new GetManagerDTO(
                            manager.Id,
                            (manager.FirstName + " " + manager.LastName)
                        )
                    );
                return managerList;
            }
            catch (Exception e) { throw; }
        }

        public async Task<GetEmployeeForeignDTO> GetEmployeeForeignByIdAsync(int id)
        {
            try
            {
                var employee = _dbContext.Employees
                        .Include(emp => emp.Manager)
                        .Include(emp => emp.Roles)
                        .Include(emp => emp.Gender)
                        .FirstOrDefault(emp => emp.Id == id);

                var employeeDTO = new GetEmployeeForeignDTO
                    (
                        id,
                        employee.FirstName,
                        employee.LastName,
                        employee.EmailAddress,
                        employee.ContactNumber,
                        (long)employee.GenderId,
                        employee.Gender.GenderName,
                        (long)employee.RoleId,
                        employee.Roles.RoleName,
                        (long)employee.ManagerId,
                        employee.Manager.FirstName + " " + employee.Manager.LastName
                    );
                return employeeDTO;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task<(IEnumerable<GetEmployeeForeignDTO>, int)> GetAllEmployeeForeignAsync(int page, int pageSize, string? sortColumn, string? sortOrder, GetEmployeeDTO employeeDTO)
        {
            try
            {
                var employeeObj = new Employee()
                {
                    FirstName = employeeDTO.FirstName,
                    LastName = employeeDTO.LastName,
                    //  Password = employeeDTO.Password,
                    EmailAddress = employeeDTO.EmailAddress,
                    ContactNumber = employeeDTO.ContactNumber,
                    GenderId = employeeDTO.GenderId,
                    RoleId = employeeDTO.RoleId,
                    ManagerId = employeeDTO.ManagerId,
                    CreatedById = employeeDTO.CreatedById,
                    JoiningDate = DateOnly.FromDateTime(DateTime.Now),
                };
                var employeeList = await _employeeRepository.GetAllEmployee(page, pageSize, sortColumn, sortOrder, employeeObj);

                var list = employeeList.Item1;


                var result = list.Select
                    (
                    employee =>
                        new GetEmployeeForeignDTO
                            (
                                employee.Id,
                                employee.FirstName,
                                employee.LastName,
                                employee.EmailAddress,
                                employee.ContactNumber,
                                (long)employee.GenderId,
                                employee.Gender.GenderName,
                                (long)employee.RoleId,
                                employee.Roles.RoleName,
                                (long)employee.ManagerId,
                                employee.Manager.FirstName + " " + employee.Manager.LastName
                            )
                    );

                return (result, employeeList.Item2);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
