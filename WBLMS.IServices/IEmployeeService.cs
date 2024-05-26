using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBLMS.DTO;
using WBLMS.Models;

namespace WBLMS.IServices
{
    public interface IEmployeeService
    {
        Task<(IEnumerable<GetEmployeeDTO>,int)> GetAllEmployeeAsync(int page, int pageSize, string? sortColumn, string? sortOrder, GetEmployeeDTO employee);
        Task<(IEnumerable<GetEmployeeLeaveReqDTO>,int)> GetAllEmployeeLeaveAsync(int page, int pageSize, string? sortColumn, string? sortOrder, GetEmployeeLeaveReqDTO employee);
        Task<(IEnumerable<GetEmployeeForeignDTO>,int)> GetAllEmployeeForeignAsync(int page, int pageSize, string? sortColumn, string? sortOrder, GetEmployeeDTO employee);

        Task<GetEmployeeDTO> GetEmployeeByIdAsync(int id);
        
        Task<GetEmployeeForeignDTO> GetEmployeeForeignByIdAsync(int id);

        Task<IEnumerable<Gender>> GetAllGenderAsync();
        Task<IEnumerable<Roles>> GetAllRolesAsync();
        Task<IEnumerable<GetManagerDTO>> GetAllManagersAsync(long id);

        Task<GetEmployeeDTO> CreateEmployeeAsync(CreateEmployeeDTO employeeDTO);

        Task<GetEmployeeDTO> UpdateEmployeeAsync(UpdateEmployeeDTO employeeDTO, int id);

        Task<bool> DeleteEmployeeAsync(int id);

        Task<Employee> GetEmployeeByEmailAsync(string email);

        Task<Token> GetTokenByEmployeeIdAsync(long id);


    }
}
