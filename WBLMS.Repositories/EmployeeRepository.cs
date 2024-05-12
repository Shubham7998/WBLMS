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

        public async Task<(IEnumerable<Employee>, int)> GetAllEmployee(int page, int pageSize, string? sortColumn, string? sortOrder, Employee employee)
        {
            var query = _dbContext.Employees.AsQueryable();
            query = SearchEmployee(query, employee);

            query = SortEmployee(query, sortOrder, sortColumn);

            (query,int totalCount) = Pagination(query, page, pageSize);

            return (await query.ToListAsync(), totalCount);
        }

        private static  (IQueryable<Employee> , int ) Pagination(IQueryable<Employee> query, int page, int pageSize)
        {
            int totalCount = query.Count();
            int totalPages = (int) Math.Ceiling((decimal) totalCount / pageSize);
            query = query.Skip((page - 1) * pageSize).Take(pageSize);

            return (query ,totalCount);
        }

        private IQueryable<Employee> SortEmployee(IQueryable<Employee> query, string? sortOrder, string? sortColumn)
        {
            bool sortInAsc = sortOrder.ToLower() == "asc";

            switch(sortOrder.ToLower()) 
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
            }
            return query;
        }

        private IQueryable<Employee> SearchEmployee(IQueryable<Employee> query, Employee employeeObj)
        {
            if(!string.IsNullOrWhiteSpace(employeeObj.FirstName))
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

            return query;
        }
    }
}