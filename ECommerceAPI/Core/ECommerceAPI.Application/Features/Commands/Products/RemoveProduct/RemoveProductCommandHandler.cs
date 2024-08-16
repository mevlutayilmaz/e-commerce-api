using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Commands.Products.RemoveProduct
{
    public class RemoveProductCommandHandler : IRequestHandler<RemoveProductCommandRequest, RemoveProductCommandResponse>
    {
        readonly IWriteRepository<Product> _writeRepository;

        public RemoveProductCommandHandler(IWriteRepository<Product> writeRepository)
        {
            _writeRepository = writeRepository;
        }

        public async Task<RemoveProductCommandResponse> Handle(RemoveProductCommandRequest request, CancellationToken cancellationToken)
        {
            await _writeRepository.RemoveAsync(request.Id);
            await _writeRepository.SaveAsync();
            return new();
        }
    }
}
