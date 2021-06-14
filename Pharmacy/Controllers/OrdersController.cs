using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PayPalCheckoutSdk.Orders;
using PayPalHttp;
using Pharmacy.Controllers.BaseControllers;
using Pharmacy.Helpers;
using Pharmacy.Models.Data_Transfrom_Objects.Order;
using Pharmacy.Services;

namespace Pharmacy.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : AuthControllerBase
    {
        private readonly OrdersService m_ordersService;

        public OrdersController(OrdersService ordersService)
        {
            m_ordersService = ordersService;
        }

        [HttpGet("verify/{orderId}")]
        public async Task<ActionResult> Verify(string orderId)
        {
            if (orderId == null)
            {
                return BadRequest();
            }
            Console.WriteLine(orderId);
            OrdersGetRequest request = new OrdersGetRequest(orderId);
            var response = await PayPalClient.client().Execute(request);
            var result = response.Result<Order>();

            Console.WriteLine("Retrieved Order Status");
            Console.WriteLine("Status: {0}", result.Status);
            Console.WriteLine("Order Id: {0}", result.Id);
            Console.WriteLine("Intent: {0}", result.CheckoutPaymentIntent);
            Console.WriteLine("Links:");
            foreach (LinkDescription link in result.Links)
            {
                Console.WriteLine("\t{0}: {1}\tCall Type: {2}", link.Rel, link.Href, link.Method);
            }
            AmountWithBreakdown amount = result.PurchaseUnits[0].AmountWithBreakdown;
            Console.WriteLine("Total Amount: {0} {1}", amount.CurrencyCode, amount.Value);

            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderReadDto>>> GetOrdersForClient()
		{
            var orders = await m_ordersService.GetOrdersForUser(GetUID());

            if (orders == null)
			{
                return NotFound();
			}

            return Ok(orders);
		}

        [HttpGet("{id}", Name = nameof(GetOrderById))]
        public async Task<ActionResult<OrderReadDto>> GetOrderById(int id)
		{
            var order = await m_ordersService.GetOrderById(id);

            if (order == null)
			{
                return NotFound();
			}

            return Ok(order);
        }

        [HttpPost]
        public async Task<ActionResult> CreateOrder(OrderCreateDto dto)
		{
            var uid = GetUID();
            
            try
            {
                OrdersGetRequest request = new OrdersGetRequest(dto.TransactionId);
                var response = await PayPalClient.client().Execute(request);
                var result = response.Result<Order>();
            } 
            catch (Exception)
            {
				return BadRequest();
            }

            var order = await m_ordersService.CreateOrder(dto, uid);
            if (order == null)
			{
                return BadRequest();
			}

            return CreatedAtAction(nameof(GetOrderById), new { id = order.Id }, null);
        }

        [HttpPatch("{id}", Name = nameof(CompleteOrder))]
        public async Task<ActionResult> CompleteOrder(int id)
		{
            if (!await m_ordersService.CompleteOrder(id))
			{
                return NotFound();
			}

            return Ok();
		}
    }
}
