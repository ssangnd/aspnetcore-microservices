//using EventBus.Messages;
using AutoMapper;
using EventBus.Messages.IntegrationEvents.Events;
using MediatR;
using Ordering.Application.Common.Mappings;
using Ordering.Domain.Entities;
using Shared.DTOs.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.V1.Orders
{
    public class CreateOrderCommand : CreateOrUpdateCommand, 
        IRequest<ApiResult<long>>, IMapFrom<Order>
        , IMapFrom<BasketCheckoutEvent>
    {
        public string UserName { get; set; }
        //public void Mapping(MappingProfile profile)
        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateOrderCommand, Order>();
            profile.CreateMap<BasketCheckoutEvent, CreateOrderCommand>();
            //.ReverseMap();
        }

    }
}
