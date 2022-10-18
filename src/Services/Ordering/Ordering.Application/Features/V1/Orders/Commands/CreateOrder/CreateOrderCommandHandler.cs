

using AutoMapper;
using Contracts.Services;
using Infrastructure.Services;
using MediatR;
using Ordering.Application.Common.Interfaces;
using Ordering.Domain.Entities;
using Serilog;
using Shared.DTOs.Configurations;
using Shared.Services.Email;

namespace Ordering.Application.Features.V1.Orders
{
    public class CreateOrdercommandhandler : IRequestHandler<CreateOrderCommand, ApiResult<long>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly ISmtpEmailService _emailService;
        private readonly ILogger _logger;
        public CreateOrdercommandhandler(IOrderRepository orderrepository,
            IMapper mapper, ISmtpEmailService emailservice, ILogger logger)
        {
            _orderRepository = orderrepository ?? throw new ArgumentNullException(nameof(orderrepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _emailService = emailservice ?? throw new ArgumentNullException(nameof(emailservice));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        private const string methodname = "CreateOrderCommandHandler";
        public async Task<ApiResult<long>> Handle(CreateOrderCommand request, CancellationToken cancellationtoken)
        {
            _logger.Information($"begin: {methodname} - username: {request.UserName}");
            var orderentity = _mapper.Map<Order>(request);
            var addedorder = await _orderRepository.CreateOrderAsync(orderentity);
            await _orderRepository.SaveChangesAsync();
            _logger.Information($"order {addedorder.Id} -Document No:{orderentity.DocumentNo} is successfully created.");
            SendEmailAsync(addedorder, cancellationtoken);
            _logger.Information($"end: {methodname} -username: {request.UserName}");
            return new ApiSuccessResult<long>(addedorder.Id);
        }

        private async Task SendEmailAsync(Order order, CancellationToken cancellationtoken)
        {
            var emailaddress = new MailRequest
            {
                ToAddress = order.EmailAddress,
                Body = "Order was created.",
                Subject = "Order was created",
            };
            try
            {
                await _emailService.SendEmailAsync(emailaddress, cancellationtoken);
                _logger.Information($"sent created order to email {order.EmailAddress}");
            }
            catch (Exception ex)
            {
                _logger.Error($"order {order.Id} failed due to an error with the email services: {ex.Message}");
            }

        }
    }
}
