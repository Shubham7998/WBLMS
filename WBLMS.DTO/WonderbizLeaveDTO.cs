using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBLMS.DTO
{
    public record GetWonderbizLeaveDTO
        (
            long Id,
            DateOnly Date,
            string Day,
            string Event
        );

}
