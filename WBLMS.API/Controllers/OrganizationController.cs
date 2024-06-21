using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WBLMS.DTO;
using WBLMS.IServices;
using WBLMS.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WBLMS.API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/organization")]
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        private readonly IOrganizationService _organizationService;

        public OrganizationController(IOrganizationService organizationService)
        {
            _organizationService = organizationService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Organization>>> GetOrganizations()
        {
            var orgs = await _organizationService.GetAllOrganizationDto();
            return Ok(orgs);    
        }

        [HttpGet("{organizationId}", Name = "GetOrganizationRoute")]
        public async Task<ActionResult<OrganizationReadDto>> GetOrganization(int organizationId)
        {
            if(!await _organizationService.OrganizationExists(organizationId))
            {
                return NotFound();
            }
            return await _organizationService.GetIndividualOrganization(organizationId);
        }

        [HttpPost]
        public async Task<ActionResult<OrganizationReadDto>> CreateOrganization(OrganizationCreateDto organizationCreateDto)
        {
            var org = await _organizationService.CreateOrganization(organizationCreateDto);
            return Ok(org);
        }

        [HttpPut("{organizationId}")]
        public async Task<ActionResult<OrganizationReadDto>> UpdateOrganization(int organizationId, OrganizationUpdateDto organizationUpdateDto)
        {
            if(organizationId != organizationUpdateDto.Id)
            {
                return BadRequest();
            }
            if(! await _organizationService.OrganizationExists(organizationId))
            {
                return NotFound();
            }
            var result = await _organizationService.UpdateOrganization(organizationUpdateDto);
            return Ok(result);
        }

        [HttpDelete("{organizationId}")]
        public async Task<ActionResult<bool>> DeleteOrganization(int organizationId)
        {
            if(! await _organizationService.OrganizationExists(organizationId)) 
            { 
                return NotFound();
            }
            return await _organizationService.DeleteOrganization(organizationId);
        }
    }
}
