using Contracts.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Common.Models;
using Ordering.Application.Features.V1.Queries.GetOrders;
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
    }
}
