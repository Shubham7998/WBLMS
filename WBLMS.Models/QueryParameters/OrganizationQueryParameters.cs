using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBLMS.Models.QueryParameters
{
    public class OrganizationQueryParameters : QueryStringParameters
    {
        public OrganizationQueryParameters() 
        {
            OrderBy = "Name";
        }

        public string? Name { get; set; }

    }
}
