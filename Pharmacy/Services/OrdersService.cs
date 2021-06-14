using Pharmacy.Models.Converters;
using Pharmacy.Models.Data_Transfrom_Objects.Order;
using Pharmacy.Models.Database.Entities;
using Pharmacy.Models.Database.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Services
{
	public class OrdersService
	{
		private readonly IAddressesRepo m_addressesRepo;
		private readonly ICartRepo m_cartRepo;
		private readonly IOrdersRepo m_ordersRepo;
		private readonly IProductsRepo m_productsRepo;
		private readonly CartService m_cartService;

		public OrdersService(IAddressesRepo addressesRepo, ICartRepo cartRepo, IOrdersRepo ordersRepo, IProductsRepo productsRepo, CartService cartService)
		{
			m_addressesRepo = addressesRepo;
			m_cartRepo = cartRepo;
			m_ordersRepo = ordersRepo;
			m_cartService = cartService;
		}

		public async Task<Order> CreateOrder(OrderCreateDto dto, string userId)
		{
			if (dto == null)
			{
				return null;
			}

			if (await m_ordersRepo.ContainsTransaction(dto.TransactionId))
			{
				return null;
			}

			if (!await m_cartService.ValidateCart(userId))
			{
				return null;
			}	

			var shippingAddress = await m_addressesRepo.GetOrCreateAddress(
				dto.ShippingAddress.City,
				dto.ShippingAddress.PostalCode,
				dto.ShippingAddress.StreetAndBuildingNo,
				dto.ShippingAddress.LocalNo);

			var billingAddress = dto.BillingAddress != null ?
				await m_addressesRepo.GetOrCreateAddress(
					dto.BillingAddress.City,
					dto.BillingAddress.PostalCode,
					dto.BillingAddress.StreetAndBuildingNo,
					dto.BillingAddress.LocalNo) : 
				null;

			var cartItems = await m_cartRepo.GetByClientId(userId, true);

			Order order = new Order
			{
				ClientId = userId,
				CreationDate = DateTime.Now,
				CompletionDate = null,
				BillingAddress = billingAddress,
				ShippingAddress = shippingAddress,
				Notes = dto.Notes,
				TransactionId = dto.TransactionId,
				RecipientName = dto.RecipientName,
				Products = new List<OrderProduct>()
			};

			await m_ordersRepo.CreateOrder(order);

			foreach (var it in cartItems)
			{
				order.Products.Add(new OrderProduct
				{
					ProductId = it.ProductId,
					Amount = it.Amount,
					Order = order
				});
			}

			foreach (var it in cartItems)
			{
				it.Product.Supply -= it.Amount;
				m_productsRepo.MarkForUpdate(it.Product);
			}

			await m_cartRepo.RemoveClientItems(userId);

			await m_productsRepo.SaveChanges();
			await m_ordersRepo.SaveChanges();
			await m_addressesRepo.SaveChanges();
			await m_cartRepo.SaveChanges();
			return order;
		}

		public async Task<IEnumerable<OrderReadDto>> GetOrdersForUser(string userId)
		{
			return OrderConverter.ToOrderReadDtos(await m_ordersRepo.GetOrdersForUser(userId));
		}

		public async Task<OrderReadDto> GetOrderById(int id)
		{
			return OrderConverter.ToOrderReadDto(await m_ordersRepo.GetOrderById(id));
		}

		public async Task<bool> CompleteOrder(int id)
		{
			var order = await m_ordersRepo.GetOrderById(id);

			if (order == null || order.CompletionDate != null)
			{
				return false;
			}

			order.CompletionDate = DateTime.Now;
			m_ordersRepo.MarkForUpdate(order);
			return (await m_ordersRepo.SaveChanges()) != 0;
		}
	}
}
