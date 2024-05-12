using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBLMS.DTO;

namespace WBLMS.IServices
{
    public interface IEmployeeService
    {
        Task<(IEnumerable<GetEmployeeDTO>,int)> GetAllEmployeeAsync();

        Task<GetEmployeeDTO> GetEmployeeByIdAsync(int id);

        Task<GetEmployeeDTO> CreateEmployeeAsync(CreateEmployeeDTO employeeDTO);

        Task<GetEmployeeDTO> UpdateEmployeeAsync(UpdateEmployeeDTO employeeDTO, int id);

        Task<bool> DeleteEmployeeAsync(int id);
    }
}
