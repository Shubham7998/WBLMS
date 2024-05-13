using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBLMS.Models
{
    public class Paginated<T> where T : class
    {
        public IEnumerable<T> dataArray { get; set; }
        public int totalPages { get; set; }
    }
}
