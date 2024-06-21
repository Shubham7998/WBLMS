using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBLMS.Data;
using WBLMS.IRepositories;
using WBLMS.Models;

namespace WBLMS.Repositories
{
    public class OrganizationRepository : Repository<Organization>, IOrganizationRepository
    {
        private readonly WBLMSDbContext _dbContext;
        public OrganizationRepository(WBLMSDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public void CreateOrganization(Organization organization)
        {
            try
            {
                if (organization == null)
                {
                    throw new ArgumentNullException(nameof(organization));
                }
                _dbContext.Add(organization);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Error While saving organization {ex.Message}");
            }
        }

        public async Task<IEnumerable<Organization>> GetAllOrganizations()
        {
            var orgsWithBranches = _dbContext.Organizations
                .Include(o => o.Branches).ThenInclude(o => o.Departments)
                .ToList();
            return orgsWithBranches;
        }

        public async Task<bool> OrganizationExists(int organizationId)
        {
            return await _dbContext.Organizations.AnyAsync(o => o.Id == organizationId);
        }
    }
}
