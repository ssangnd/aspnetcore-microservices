using AutoMapper;
using MediatR;
using Ordering.Application.Common.Exceptions;
using Ordering.Application.Common.Interfaces;
using Ordering.Application.Common.Models;
using Ordering.Domain.Entities;
using Serilog;
using Shared.DTOs.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.V1.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, ApiResult<OrderDto>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public UpdateOrderCommandHandler(IOrderRepository orderRepository, ILogger logger, IMapper mapper)
        {           
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper)); 
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
           
        }

        private const string MethodName = "UpdateOrderCommandHandler";
        public async Task<ApiResult<OrderDto>> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var orderEntity = await _orderRepository.GetByIdAsync(request.Id);
            if (orderEntity is null) throw new NotFoundException(nameof(Order), request.Id);
            _logger.Information($"BEGIN: {MethodName} - Order: {request.Id}");
            orderEntity = _mapper.Map(request, orderEntity);
            var updatedOrder = await _orderRepository.UpdateOrderAsync(orderEntity);
            _orderRepository.SaveChangesAsync();
            _logger.Information($"Orderr {request.Id} was successfully updated.");
            var result = _mapper.Map<OrderDto>(updatedOrder);

            _logger.Information($"END: {MethodName} -Order: {request.Id}");
            return new ApiSuccessResult<OrderDto>(result);
        }
    }
}
