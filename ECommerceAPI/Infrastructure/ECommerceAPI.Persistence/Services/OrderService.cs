using ECommerceAPI.Application.DTOs.Orders;
using ECommerceAPI.Application.Interfaces.Services;
using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities;

namespace ECommerceAPI.Persistence.Services
{
    public class OrderService : IOrderService
    {
        readonly IWriteRepository<Order> _orderWriteRepository;

        public OrderService(IWriteRepository<Order> orderWriteRepository)
        {
            _orderWriteRepository = orderWriteRepository;
        }

        public async Task CreateOrderAsync(CreateOrderDTO createOrder)
        {
            await _orderWriteRepository.AddAsync(new()
            {
                Id = Guid.Parse(createOrder.BasketId),
                Address = createOrder.Address,
                Description = createOrder.Description,
            });

            await _orderWriteRepository.SaveAsync();
        }
    }
}
