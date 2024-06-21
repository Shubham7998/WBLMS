using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBLMS.IRepositories;
using WBLMS.IServices;
using WBLMS.Models;

namespace WBLMS.Services
{
    public class OrganizationService : IOrganizationService
    {
        private readonly IOrganizationRepository _organizationRepository;

        public OrganizationService(IOrganizationRepository organizationRepository)
        {
            _organizationRepository = organizationRepository;
        }

        public async Task<IEnumerable<Organization>> GetAllOrganizations()
        {
            var orgs = await _organizationRepository.GetAllOrganizations();
            return orgs;
        }
    }
}
