using MediatR;
using Ordering.Application.Common.Models;
using Shared.DTOs.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.V1.Queries.GetOrders
{
    public class GetOrdersQuery :IRequest<ApiResult<List<OrderDto>>>
    {
        public string UserName { get;private set; }
        public GetOrdersQuery(string userName)
        {
            UserName = userName ?? throw new ArgumentNullException(nameof(userName));
        }
    }
}
