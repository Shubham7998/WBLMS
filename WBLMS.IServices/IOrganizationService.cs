using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBLMS.DTO;
using WBLMS.Models;
using WBLMS.Models.QueryParameters;

namespace WBLMS.IServices
{
    public interface IOrganizationService
    {
        Task<bool> OrganizationExists(int id);
        Task<(IEnumerable<OrganizationReadDto>, PaginationMetadata)> GetAllOrganizationDto(string? search, OrganizationQueryParameters orgQParam);
        Task<OrganizationReadDto> CreateOrganization(OrganizationCreateDto organizationCreateDto);
        Task<OrganizationReadDto> GetIndividualOrganization(int organizationId);
        Task<OrganizationReadDto> UpdateOrganization(OrganizationUpdateDto organizationUpdateDto);
        Task<bool> DeleteOrganization(int organizationId);
    }
}
