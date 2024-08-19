using ECommerceAPI.Application.DTOs.Orders;
using ECommerceAPI.Application.Interfaces.Services;
using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Persistence.Services
{
    public class OrderService : IOrderService
    {
        readonly IWriteRepository<Order> _orderWriteRepository;
        readonly IReadRepository<Order> _orderReadRepository;
        readonly IWriteRepository<CompletedOrder> _completedOrderWriteRepository;

        public OrderService(IWriteRepository<Order> orderWriteRepository, IReadRepository<Order> orderReadRepository, IWriteRepository<CompletedOrder> completedOrderWriteRepository)
        {
            _orderWriteRepository = orderWriteRepository;
            _orderReadRepository = orderReadRepository;
            _completedOrderWriteRepository = completedOrderWriteRepository;
        }

        public async Task CreateOrderAsync(CreateOrderDTO createOrder)
        {
            await _orderWriteRepository.AddAsync(new()
            {
                Id = Guid.Parse(createOrder.BasketId),
                Address = createOrder.Address,
                Description = createOrder.Description,
                OrderCode = (new Random().NextDouble() * 100000000000).ToString().Substring(0, 11),
            });

            await _orderWriteRepository.SaveAsync();
        }

        public async Task<OrderListDTO> GetAllOrdersAsync(int pageCount, int itemCount)
        {
            var query = _orderReadRepository.Table
               .Include(o => o.CompletedOrder)
               .Include(o => o.Basket)
               .ThenInclude(b => b.User)
               .Include(o => o.Basket)
               .ThenInclude(b => b.BasketItems)
               .ThenInclude(bi => bi.Product);

            var datas = query.Skip(itemCount * (pageCount - 1)).Take(itemCount);

            return new()
            {
                TotalCount = await query.CountAsync(),
                Orders = await datas.Select(o => new
                {
                    Id = o.Id,
                    CreatedDate = o.CreatedDate,
                    OrderCode = o.OrderCode,
                    TotalPrice = o.Basket.BasketItems.Sum(bi => bi.Product.Price * bi.Quantity),
                    UserName = o.Basket.User.UserName,
                    Completed = o.CompletedOrder != null ? true : false
                }).ToListAsync()
            };

        }

        public async Task<SingleOrderDTO> GetOrderByIdAsync(string id)
        {
            var data = await _orderReadRepository.Table
                .Include(o => o.CompletedOrder)
                .Include(o => o.Basket)
                .ThenInclude(b => b.BasketItems)
                .ThenInclude(bi => bi.Product)
                .FirstOrDefaultAsync(o => o.Id == Guid.Parse(id));

            return new()
            {
                Id = data.Id,
                CreatedDate = data.CreatedDate,
                OrderCode = data.OrderCode,
                Address = data.Address,
                Description = data.Description,
                Completed = data.CompletedOrder != null ? true : false,
                BasketItems = data.Basket.BasketItems.Select(bi => new
                {
                    Name = bi.Product.Name,
                    Price = bi.Product.Price,
                    ImageUrl = bi.Product.ImageUrl,
                    Quantity = bi.Quantity,
                })
            };
        }

        public async Task<(bool, CompletedOrderDTO)> CompleteOrderAsync(string id)
        {
            var order = await _orderReadRepository.Table
                .Include(o => o.Basket)
                .ThenInclude(b => b.User)
                .Include(o => o.Basket)
                .ThenInclude(b => b.BasketItems)
                .ThenInclude(bi => bi.Product)
                .FirstOrDefaultAsync(o => o.Id == Guid.Parse(id));
            if(order != null)
            {
                await _completedOrderWriteRepository.AddAsync(new() { Id = Guid.Parse(id) });
                return (await _completedOrderWriteRepository.SaveAsync() > 0, new ()
                {
                    OrderCode = order.OrderCode,
                    OrderDate = order.CreatedDate,
                    Email = order.Basket.User.Email,
                    UserName = order.Basket.User.UserName,
                    BasketItems = order.Basket.BasketItems.ToList(),
                });
            }
            return (false, null);
        }
    }
}
