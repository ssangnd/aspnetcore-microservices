using AutoMapper;
using Infrastructure.Mapping;
using MediatR;
using Ordering.Application.Common.Mappings;
using Ordering.Application.Common.Models;
using Ordering.Domain.Entities;
using Shared.DTOs.Configurations;

namespace Ordering.Application.Features.V1.Orders
{
    public class UpdateOrderCommand:CreateOrUpdateCommand, IRequest<ApiResult<OrderDto>>, IMapFrom<Order>
    {
        public long Id { get; set; }

        public void SetId(long id)
        {
            Id = id;
        }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateOrderCommand, Order>()
                .ForMember(dest => dest.Status, opts => opts.Ignore())
                .IgnoreAllNonExisting();
        }
    }
}
