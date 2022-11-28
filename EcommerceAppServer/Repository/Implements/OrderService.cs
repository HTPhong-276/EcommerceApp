using Domain.Entity;
using Domain.Entity.OrderAggregate;
using Repository.Interfaces;
using Repository.Specifications.ModelSpecification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implements
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IBasketRepository basketRepo;

        public OrderService(
            IUnitOfWork unitOfWork,
            IBasketRepository basketRepo)
        {
            this.unitOfWork = unitOfWork;
            this.basketRepo = basketRepo;
        }

        public async Task<Order> CreateOrderAsync(string buyerEmail, int deliverMethodId, string basketId, Address shippingAddress)
        {
            var basket = await basketRepo.GetBasketAsync(basketId);

            var items = new List<OrderItem>();
            foreach(var item in basket.Items)
            {
                var productItem = await unitOfWork.Repository<Product>().GetByIdAsync(item.ItemId);
                var itemOrderd = new ProductItemOrdered(
                    productItem.ProductId,
                    productItem.Name,
                    productItem.PictureUrl);
                var orderItem = new OrderItem(itemOrderd, productItem.Price, item.Quantity);
                items.Add(orderItem);
            }

            var deliverMethod = await unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliverMethodId);

            var subTotal = items.Sum(i => i.Price * i.Quantity);

            var order = new Order(items, buyerEmail, shippingAddress, deliverMethod, subTotal);

            unitOfWork.Repository<Order>().add(order);

            var result = await unitOfWork.Complete();

            if(result <= 0) return null;

            await basketRepo.DeleteBasketAsync(basketId);

            return order;
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            return await unitOfWork.Repository<DeliveryMethod>().ListAllAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int id, string buyerEmail)
        {
            var spec = new OrdersWithItemsAndOrderingSpecification(id, buyerEmail);

            return await unitOfWork.Repository<Order>().GetWithSpec(spec);
        }

        public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            var spec = new OrdersWithItemsAndOrderingSpecification(buyerEmail);

            return await unitOfWork.Repository<Order>().ListAsync(spec);
        }
    }
}
