using AutoMapper;
using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Commands.Products.CreateProducts
{
    public class CreateProductsCommandHandler : IRequestHandler<CreateProductsCommandRequest, CreateProductsCommandResponse>
    {
        readonly IWriteRepository<Product> _writeRepository;
        readonly IMapper _mapper;

        public CreateProductsCommandHandler(IWriteRepository<Product> writeRepository, IMapper mapper)
        {
            _writeRepository = writeRepository;
            _mapper = mapper;
        }

        public async Task<CreateProductsCommandResponse> Handle(CreateProductsCommandRequest request, CancellationToken cancellationToken)
        {
            await _writeRepository.AddRangeAsync(_mapper.Map<List<Product>>(request.Products));

            await _writeRepository.SaveAsync();
            return new();
        }
    }
}
