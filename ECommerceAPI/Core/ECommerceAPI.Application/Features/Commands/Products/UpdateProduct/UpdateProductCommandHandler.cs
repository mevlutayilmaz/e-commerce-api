using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Commands.Products.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest, UpdateProductCommandResponse>
    {
        readonly IWriteRepository<Product> _writeRepository;
        readonly IReadRepository<Product> _readRepository;

        public UpdateProductCommandHandler(IWriteRepository<Product> writeRepository, IReadRepository<Product> readRepository)
        {
            _writeRepository = writeRepository;
            _readRepository = readRepository;
        }

        public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            Product product = await _readRepository.GetByIdAsync(request.Id, true);
            product.Price = request.Price;
            await _writeRepository.SaveAsync();
            return new();
        }
    }
}
