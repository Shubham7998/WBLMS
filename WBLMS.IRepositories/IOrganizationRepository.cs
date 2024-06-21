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
        Task<IEnumerable<Organization>> GetAllOrganizations();
        Task<bool> OrganizationExists (int  organizationId);
        void CreateOrganization(Organization organization);
        
    }
}
