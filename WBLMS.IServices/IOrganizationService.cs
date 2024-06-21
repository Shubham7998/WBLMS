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
        Task<IEnumerable<Organization>> GetAllOrganizations();
    }
}
