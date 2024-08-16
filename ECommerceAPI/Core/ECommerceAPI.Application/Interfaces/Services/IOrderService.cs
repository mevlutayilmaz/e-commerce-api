using ECommerceAPI.Application.DTOs.Orders;

namespace ECommerceAPI.Application.Interfaces.Services
{
    public interface IOrderService
    {
        public Task CreateOrderAsync(CreateOrderDTO createOrder);
    }
}
