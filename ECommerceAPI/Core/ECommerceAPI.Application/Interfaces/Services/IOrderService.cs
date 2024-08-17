using ECommerceAPI.Application.DTOs.Orders;

namespace ECommerceAPI.Application.Interfaces.Services
{
    public interface IOrderService
    {
        public Task CreateOrderAsync(CreateOrderDTO createOrder);
        public Task<OrderListDTO> GetAllOrdersAsync(int pageCount, int itemCount);
        public Task<SingleOrderDTO> GetOrderByIdAsync(string id);

    }
}
