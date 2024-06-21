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

        public async Task<IEnumerable<Organization>> GetAllOrganizations()
        {
            var orgsWithBranches = _dbContext.Organizations
                .Include(o => o.Branches).ThenInclude(o => o.Departments)
                .ToList();
            return orgsWithBranches;
        }
    }
}
