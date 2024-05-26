using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBLMS.DTO;
using WBLMS.Models;

namespace WBLMS.IRepositories
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        // Task<IEnumerable<GetEmployeeDTO>> GetAllEmployeeForeignAsync();

        Task<(IEnumerable<Employee>, int)> GetAllEmployee(int page, int pageSize, string? sortColumn, string? sortOrder, Employee employee);
        Task<(IEnumerable<Employee>, int)> GetAllEmployeeLeaveReq(int page, int pageSize, string? sortColumn, string? sortOrder, Employee employee);

        Task<Employee> GetEmployeeByEmail(string email);

        Task<Token> GetTokenAsync(long id);
    }
}
