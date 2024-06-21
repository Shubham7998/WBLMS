using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBLMS.DTO;
using WBLMS.Models;
using WBLMS.Models.QueryParameters;

namespace WBLMS.IRepositories
{
    public interface IOrganizationRepository : IRepository<Organization>
    {
        Task<(PagedList<Organization>, PaginationMetadata)> GetAllOrganizations(string? search, OrganizationQueryParameters orgQParam);
        Task<bool> OrganizationExists (int  organizationId);
        void CreateOrganization(Organization organization);
        
    }
}
