using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBLMS.Models;

namespace WBLMS.IRepositories
{
    public interface IOrganizationRepository : IRepository<Organization>
    {
        Task<(PagedList<Organization>, PaginationMetadata)> GetAllOrganizations(int page, int pageSize, string? search);
        Task<bool> OrganizationExists (int  organizationId);
        void CreateOrganization(Organization organization);
        
    }
}
