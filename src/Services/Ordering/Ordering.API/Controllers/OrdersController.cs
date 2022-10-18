using Contracts.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Common.Models;
using Ordering.Application.Features.V1.Orders;
using Ordering.Domain.Entities;
using Shared.DTOs.Configurations;
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

        public OrdersController(IMediator mediator
            //,ISmtpEmailService emailService
            )
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            //_emailService = emailService;
        }
        private static class RouteNames
        {
            public const string GetOrders = nameof(GetOrders);
            public const string GetOrdersPagination = nameof(GetOrdersPagination);
            public const string CreateOrder = nameof(CreateOrder);
            public const string UpdateOrder = nameof(UpdateOrder);
            public const string DeleteOrder = nameof(DeleteOrder);
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

        #region CRUD
        //[HttpGet("", Name = RouteNames.GetOrdersPagination)]
        //[ProducesResponseType(typeof(IEnumerable<OrderDto>), (int)HttpStatusCode.OK)]
        //public async Task<ActionResult<PagedList<Order>>> GetOrdersPagingQuery([FromBody] GetOrderPagingQuery query)
        //{
        //    var result = await _mediator.Send(query);
        //    return Ok(result);
        //}

        [HttpGet("{username}", Name = RouteNames.GetOrders)]
        [ProducesResponseType(typeof(IEnumerable<OrderDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrdersByUserName([Required] string username)
        {
            var query = new GetOrdersByUserNameQuery(username);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost(Name = RouteNames.CreateOrder)]
        [ProducesResponseType(typeof(ApiResult<long>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ApiResult<long>>> CreateOrder([FromBody] CreateOrderCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("{id:long}", Name = RouteNames.UpdateOrder)]
        [ProducesResponseType(typeof(ApiResult<OrderDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<OrderDto>> UpdateOrder([Required] long id, [FromBody] UpdateOrderCommand command)
        {
            command.SetId(id);
            var result = await _mediator.Send(command);
            return Ok(result);

        }


        [HttpDelete("{id:long}", Name = RouteNames.DeleteOrder)]
        [ProducesResponseType(typeof(NoContentResult), (int)HttpStatusCode.NoContent)]
        public async Task<ActionResult> DeleteOrder([Required] long id)
        {
            var command = new DeleteOrderCommand(id);
            await _mediator.Send(command);
            return NoContent();
        }

        #endregion
    }
}
