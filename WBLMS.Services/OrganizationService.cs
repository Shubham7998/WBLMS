using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBLMS.DTO;
using WBLMS.IRepositories;
using WBLMS.IServices;
using WBLMS.Models;

namespace WBLMS.Services
{
    public class OrganizationService : IOrganizationService
    {
        private readonly IOrganizationRepository _organizationRepository;
        private readonly IMapper _mapper;

        public OrganizationService(IOrganizationRepository organizationRepository, IMapper mapper)
        {
            _organizationRepository = organizationRepository;
            _mapper = mapper;
        }

        public async Task<OrganizationReadDto> CreateOrganization(OrganizationCreateDto organizationCreateDto)
        {
            try
            {
                if (organizationCreateDto == null)
                {
                    throw new ArgumentNullException(nameof(organizationCreateDto));
                }
                var organization = _mapper.Map<Organization>(organizationCreateDto);
                _organizationRepository.CreateOrganization(organization);

                var organizationReadDto = _mapper.Map<OrganizationReadDto>(organization);
                return organizationReadDto;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Creating Organization Service {ex.Message}");
                throw;
            }
        }

        public async Task<bool> DeleteOrganization(int organizationId)
        {
            var organization = await _organizationRepository.GetAsyncById(organizationId);
            return await _organizationRepository.DeleteAsync(organization);
        }

        public async Task<(IEnumerable<OrganizationReadDto>, PaginationMetadata)> GetAllOrganizationDto(int page, int pageSize, string search)
        {
            var orgsWithMetadata = await _organizationRepository.GetAllOrganizations(page, pageSize, search);
            var orgsDto = _mapper.Map<IEnumerable<OrganizationReadDto>>(orgsWithMetadata.Item1);
            return (orgsDto, orgsWithMetadata.Item2);
        }


        public async Task<OrganizationReadDto> GetIndividualOrganization(int organizationId)
        {
            var org = await _organizationRepository.GetAsyncById(organizationId);
            return _mapper.Map<OrganizationReadDto>(org);
        }

        public async Task<bool> OrganizationExists(int id)
        {
            var result = await _organizationRepository.OrganizationExists(id);
            return result;
        }

        public async Task<OrganizationReadDto> UpdateOrganization(OrganizationUpdateDto organizationUpdateDto)
        {
            try
            {
                var organizationObj = _mapper.Map<Organization>(organizationUpdateDto);
                var organization = await _organizationRepository.UpdateAsync(organizationObj);
                return _mapper.Map<OrganizationReadDto>(organization);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Updating Organization {ex.Message}");
                throw;
            }
        }
    }
}
