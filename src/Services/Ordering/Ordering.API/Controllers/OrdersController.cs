using AutoMapper;
using Contracts.Messages;
using Contracts.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Common.Interfaces;
using Ordering.Application.Common.Models;
using Ordering.Application.Features.V1.Queries.GetOrders;
using Ordering.Domain.Entities;
using Shared.Services.Email;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Ordering.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;
        //private readonly ISmtpEmailService _emailService;
        //private readonly IEmailService<MailRequest> _emailService;
        private readonly IMessageProducer _messageProducer;

        private readonly IOrderRepository _repository;
        private readonly IMapper _mapper;



        public OrdersController(IMediator mediator
            //,ISmtpEmailService emailService
            , IMessageProducer messageProducer
            , IOrderRepository repository
, IMapper mapper)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            //_emailService = emailService;

            _messageProducer = messageProducer ?? throw new ArgumentNullException(nameof(messageProducer));
            _repository = repository;
            _mapper = mapper;
        }
        private static class RouteNames
        {
            public const string GetOrders = nameof(GetOrders);
        }

        [HttpGet("{username}",Name=RouteNames.GetOrders)]
        [ProducesResponseType(typeof(IEnumerable<OrderDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable>> GetOrdersByUserName([Required] string username)
        {
            var query = new GetOrdersQuery(username);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        //[HttpGet("test-email")]
        //public async Task <IActionResult> TestEmail()
        //{
        //    var message = new MailRequest
        //    {
        //        Body= "<h1>hello</h1>",
        //        Subject="Test",
        //        ToAddress="nds7220@gmail.com"
        //    };
        //    await _emailService.SendEmailAsync(message);
        //    return Ok();
        //}

        [HttpPost]
        public async Task<ActionResult> CreateOrder(OrderDto orderDto)
        {
            var order = _mapper.Map<Order>(orderDto);
            var addedOrder =await _repository.CreateOrder(order);
            await _repository.SaveChangesAsync();
            var result = _mapper.Map<OrderDto>(addedOrder);
            _messageProducer.SendMessage(result);
            return Ok(result);
        }
    }
}
