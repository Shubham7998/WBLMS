using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBLMS.Models;

namespace WBLMS.DTO
{

    public record OrganizationReadDto(
            int Id, string Name
        );

    public record OrganizationCreateDto(
            string Name, string HeadQuarter, int SuperAdminId
        );

    public record OrganizationUpdateDto(
            int Id, string Name, string HeadQuarter, int SuperAdminId
        );

}
