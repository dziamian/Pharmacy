using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PayPalCheckoutSdk.Orders;
using PayPalHttp;
using Pharmacy.Helpers;

namespace Pharmacy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        public OrdersController()
        {

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
    }
}
