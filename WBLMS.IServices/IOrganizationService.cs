using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBLMS.DTO;
using WBLMS.Models;

namespace WBLMS.IServices
{
    public interface IOrganizationService
    {
        Task<IEnumerable<Organization>> GetAllOrganizationsWithForeignKeysData();
        Task<bool> OrganizationExists(int id);
        Task<IEnumerable<OrganizationReadDto>> GetAllOrganizationDto();
        Task<OrganizationReadDto> CreateOrganization(OrganizationCreateDto organizationCreateDto);
        Task<OrganizationReadDto> GetIndividualOrganization(int organizationId);
        Task<OrganizationReadDto> UpdateOrganization(OrganizationUpdateDto organizationUpdateDto);
        Task<bool> DeleteOrganization(int organizationId);
    }
}
