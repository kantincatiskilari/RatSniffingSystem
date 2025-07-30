using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatSniffingSystem.Application.Common.Requests
{
    public record PagedQuery(int Page = 1, int PageSize = 50, string? Search = null, string? Sort = null, bool Desc = false);

}
