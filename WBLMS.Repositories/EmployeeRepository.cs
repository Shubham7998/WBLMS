using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBLMS.Data;
using WBLMS.DTO;
using WBLMS.IRepositories;
using WBLMS.Models;

namespace WBLMS.Repositories
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        private readonly WBLMSDbContext _dbContext;

        public EmployeeRepository(WBLMSDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Employee> GetEmployeeByEmail(string email)
        {
            try
            {
                var result = await _dbContext.Employees
                    .Include(emp => emp.Token)
                    .Include(emp => emp.Roles)
                    .Include(emp => emp.Gender)
                    .Include(emp => emp.Manager)
                    .FirstOrDefaultAsync(emp => emp.EmailAddress == email);

                if (result == null)
                {
                    return null;
                }
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<Token> GetTokenAsync(long id)
        {
            var result = _dbContext.Tokens.FirstOrDefault(token => token.EmployeeId == id);
            return result;
        }
        public async Task<(IEnumerable<Employee>, int)> GetAllEmployee(int page, int pageSize, string? sortColumn, string? sortOrder, Employee employee)
        {
            var query = _dbContext.Employees
                .Include(e => e.Manager)
                .Include(e => e.Roles)
                .Include(e => e.Gender)
                .Include(e => e.LeaveBalance)
                .AsQueryable();

            query = SearchEmployee(query, employee);

            if (!string.IsNullOrEmpty(sortOrder) && !string.IsNullOrEmpty(sortColumn))
            {
                query = SortEmployee(query, sortOrder, sortColumn);
            }

            (query, int totalCount) = Pagination(query, page, pageSize);

            return (await query.ToListAsync(), totalCount);
        }

        public async Task<(IEnumerable<Employee>, int)> GetAllEmployeeAsync(int page, int pageSize, string? sortColumn, string? sortOrder, string searchkeyword)
        {
            var query = _dbContext.Employees
                .Include(e => e.Manager)
                .Include(e => e.Roles)
                .Include(e => e.Gender)
                .Include(e => e.LeaveBalance)
                .Where(e => e.Id != 1)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchkeyword))
            {
                query = SearchEmployee(query, searchkeyword);
            }

            if (!string.IsNullOrEmpty(sortOrder) && !string.IsNullOrEmpty(sortColumn))
            {
                query = SortEmployee(query, sortOrder, sortColumn);
            }

            (query, int totalCount) = Pagination(query, page, pageSize);

            return (await query.ToListAsync(), totalCount);
        }

        private IQueryable<Employee> SearchEmployee(IQueryable<Employee> query, GetEmployeesDTO employeeObj, string searchKeyword)
        {
            if (!string.IsNullOrWhiteSpace(employeeObj.FirstName))
            {
                query = query.Where(employee => employee.FirstName.Contains(employeeObj.FirstName));
            }
            if (!string.IsNullOrWhiteSpace(employeeObj.LastName))
            {
                query = query.Where(employee => employee.LastName.Contains(employeeObj.LastName));
            }
            if (!string.IsNullOrWhiteSpace(employeeObj.EmailAddress))
            {
                query = query.Where(employee => employee.EmailAddress.Contains(employeeObj.EmailAddress));
            }
            if (!string.IsNullOrWhiteSpace(employeeObj.ContactNumber))
            {
                query = query.Where(employee => employee.ContactNumber.Contains(employeeObj.EmailAddress));
            }
            if (employeeObj.GenderId != 0)
            {
                query = query.Where(employee => employee.Gender.GenderName.Contains(employeeObj.GenderName));
            }
            if (employeeObj.RoleId != 0)
            {
                query = query.Where(employee => employee.Roles.RoleName.Contains(employeeObj.RoleName));
            }
            if (employeeObj.ManagerId != 0)
            {
                query = query.Where(employee => employee.Manager.FirstName.Contains(employeeObj.ManagerName) || employee.Manager.LastName.Contains(employeeObj.ManagerName));
            }
            if (employeeObj.BalanceLeaveRequest != 0)
            {
                query = query.Where(employee => employee.LeaveBalance.Balance == int.Parse(searchKeyword));
            }

            return query;
        }

        private IQueryable<Employee> SearchEmployee(IQueryable<Employee> query, string searchKeyword)
        {
            var result = query.Where
                (
                    emp =>
                            emp.FirstName.Contains(searchKeyword) ||
                            emp.LastName.Contains(searchKeyword) ||
                            emp.EmailAddress.Contains(searchKeyword) ||
                            emp.ContactNumber.Contains(searchKeyword) ||
                            emp.Gender.GenderName.Contains(searchKeyword) ||
                            emp.Roles.RoleName.Contains(searchKeyword) ||
                            emp.Manager.FirstName.Contains(searchKeyword) ||
                            emp.Manager.LastName.Contains(searchKeyword) ||
                            emp.JoiningDate.ToString().Contains(searchKeyword) ||
                            emp.LeaveBalance.Balance.ToString().Contains(searchKeyword)
                );
            return result;
        }

        private static (IQueryable<Employee>, int) Pagination(IQueryable<Employee> query, int page, int pageSize)
        {
            int totalCount = query.Count();
            //int totalPages = (int)Math.Ceiling((decimal)totalCount / pageSize);
            query = query.Skip((page - 1) * pageSize).Take(pageSize);

            return (query, totalCount);
        }

        private IQueryable<Employee> SortEmployee(IQueryable<Employee> query, string? sortOrder, string? sortColumn)
        {
            bool sortInAsc = sortOrder.ToLower() == "asc";

            switch (sortColumn.ToLower())
            {
                case "firstname":
                    query = sortInAsc ? query.OrderBy(s => s.FirstName) : query.OrderByDescending(s => s.FirstName);
                    break;
                case "lastname":
                    query = sortInAsc ? query.OrderBy(s => s.LastName) : query.OrderByDescending(s => s.LastName);
                    break;
                case "emailaddress":
                    query = sortInAsc ? query.OrderBy(s => s.EmailAddress) : query.OrderByDescending(s => s.EmailAddress);
                    break;
                case "contactnumber":
                    query = sortInAsc ? query.OrderBy(s => s.ContactNumber) : query.OrderByDescending(s => s.ContactNumber);
                    break;
                case "gendername":
                    query = sortInAsc ? query.OrderBy(s => s.Gender.GenderName) : query.OrderByDescending(s => s.Gender.GenderName);
                    break;
                case "rolename":
                    query = sortInAsc ? query.OrderBy(s => s.Roles.RoleName) : query.OrderByDescending(s => s.Roles.RoleName);
                    break;
                case "managername":
                    query = sortInAsc ? query.OrderBy(s => s.Manager.FirstName) : query.OrderByDescending(s => s.Manager.FirstName);
                    break;
                case "joiningdate":
                    query = sortInAsc ? query.OrderBy(s => s.JoiningDate) : query.OrderByDescending(s => s.JoiningDate);
                    break;
                case "leavebalance":
                    query = sortInAsc ? query.OrderBy(s => s.LeaveBalance.Balance) : query.OrderByDescending(s => s.LeaveBalance.Balance);
                    break;
                default:
                    return query;
            }
            return query;
        }

        private IQueryable<Employee> SearchEmployee(IQueryable<Employee> query, Employee employeeObj)
        {
            if (!string.IsNullOrWhiteSpace(employeeObj.FirstName))
            {
                query = query.Where(employee => employee.FirstName.Contains(employeeObj.FirstName));
            }
            if (!string.IsNullOrWhiteSpace(employeeObj.LastName))
            {
                query = query.Where(employee => employee.LastName.Contains(employeeObj.LastName));
            }
            if (!string.IsNullOrWhiteSpace(employeeObj.EmailAddress))
            {
                query = query.Where(employee => employee.EmailAddress == employeeObj.EmailAddress);
            }
            if (!string.IsNullOrWhiteSpace(employeeObj.ContactNumber))
            {
                query = query.Where(employee => employee.ContactNumber.Contains(employeeObj.ContactNumber));
            }
            if (employeeObj.GenderId != 0)
            {
                query = query.Where(employee => employee.GenderId == employeeObj.GenderId);
            }
            if (employeeObj.RoleId != 0)
            {
                query = query.Where(employee => employee.RoleId == employeeObj.RoleId);
            }
            if (employeeObj.ManagerId != 0)
            {
                query = query.Where(employee => employee.ManagerId == employeeObj.ManagerId);
            }
            if (employeeObj.CreatedById != 0)
            {
                query = query.Where(employee => employee.CreatedById == employeeObj.CreatedById);
            }

            //if(employeeObj.LeaveBalanceId != 0) 
            //{
            //    query = query.Where(employee => employee.LeaveBalance.Balance == employeeObj.LeaveBalance.Balance);
            //}

            return query;
        }

        public async Task<(IEnumerable<Employee>, int)> GetAllEmployeeLeaveReq(int page, int pageSize, string? sortColumn, string? sortOrder, Employee employee)
        {
            var query = _dbContext.Employees
                .Include(e => e.Manager)
                .Include(e => e.Roles)
                .Include(e => e.Gender)
                .Include(e => e.LeaveBalance)
                .AsQueryable();

            query = SearchEmployee(query, employee);

            if (!string.IsNullOrEmpty(sortOrder) && !string.IsNullOrEmpty(sortColumn))
            {
                query = SortEmployee(query, sortOrder, sortColumn);
            }

            (query, int totalCount) = Pagination(query, page, pageSize);

            return (await query.ToListAsync(), totalCount);
        }
    }
}