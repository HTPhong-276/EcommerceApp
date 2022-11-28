using Api.Dtos;
using Api.Errors;
using Api.Extensions;
using AutoMapper;
using Domain.Entity.OrderAggregate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces;
using System.Security.Claims;

namespace Api.Controllers
{
    [Authorize]
    public class OrderController : BaseApiController
    {
        private readonly IOrderService orderService;
        private readonly IMapper mapper;

        public OrderController(IOrderService orderService, IMapper mapper)
        {
            this.orderService = orderService;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(OrderDto orderDto)
        {
            var email = HttpContext.User.RetriveEmailFromPrinciple();

            var address = mapper.Map<AddressDto, Address>(orderDto.ShippingAddress);

            var order = await orderService.CreateOrderAsync(email, orderDto.DeliveryMethodId, orderDto.BasketId, address);

            if (order == null) return BadRequest(new ApiResponse(400, "err create order"));

            return Ok(order);
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderDto>>> GetOrderForUser()
        {
            var email = HttpContext.User.RetriveEmailFromPrinciple();

            var orders = await orderService.GetOrdersForUserAsync(email);

            return Ok(mapper.Map<IReadOnlyList<Order>, IReadOnlyList<OrderToReturnDto>>(orders));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderToReturnDto>> GetOrderById(int id)
        {
            var email = HttpContext.User.RetriveEmailFromPrinciple();

            var order = await orderService.GetOrderByIdAsync(id, email);

            if (order == null) return NotFound(new ApiResponse(404));

            return mapper.Map<Order, OrderToReturnDto>(order);
        }

        [HttpGet("deliveryMethod")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethod()
        {
            return Ok(await orderService.GetDeliveryMethodsAsync());
        }
    }
}
