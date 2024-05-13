using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
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
                        CreatedById = employeeDTO.CreatedById,
                        JoiningDate = DateOnly.FromDateTime(DateTime.Now),
                    }
                );

            return new GetEmployeeDTO
                (
                    employee.Id,
                    employee.FirstName,
                    employee.LastName,
                    employee.Password,
                    employee.EmailAddress,
                    employee.ContactNumber,
                    0,
                    employee.Gender.GenderName,
                    0,
                    employee.Roles.RoleName,
                    0,
                    employee.Manager.FirstName,
                    0,
                    employee.Manager.FirstName,
                    employee.UpdatedDate,
                    employee.Subordinates
                );

        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            try
            {
                var employee = await _employeeRepository.GetAsyncById(id);
                if(employee != null)
                {
                    await _employeeRepository.DeleteAsync(employee);    
                }
                return true;
            }catch(Exception ex)
            {
                throw;
            }
        }

        public Task<(IEnumerable<GetEmployeeDTO>, int)> GetAllEmployeeAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<GetEmployeeDTO> GetEmployeeByIdAsync(int id)
        {
            try
            {
                var employee = await _employeeRepository.GetAsyncById(id);
                return new GetEmployeeDTO
                (
                    employee.Id,
                    employee.FirstName,
                    employee.LastName,
                    employee.Password,
                    employee.EmailAddress,
                    employee.ContactNumber,
                    0,
                    employee.Gender.GenderName,
                    0,
                    employee.Roles.RoleName,
                    0,
                    employee.Manager.FirstName,
                    0,
                    employee.Manager.FirstName,
                    employee.UpdatedDate,
                    employee.Subordinates
                );
            }catch(Exception e)
            {
                throw;
            }
        }

        public Task<GetEmployeeDTO> UpdateEmployeeAsync(UpdateEmployeeDTO employeeDTO, int id)
        {
            throw new NotImplementedException();
        }
    }
}
