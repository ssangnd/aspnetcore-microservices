using MediatR;
using Ordering.Application.Common.Models;
using Shared.DTOs.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.V1.Orders
{
    public class GetOrdersByUserNameQuery : IRequest<ApiResult<List<OrderDto>>>
    {
        public string UserName { get; set; }

        public GetOrdersByUserNameQuery(string userName)
        {
            UserName = userName ?? throw new ArgumentNullException(nameof(userName));
        }
    }
}
