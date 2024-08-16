using AutoMapper;
using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Commands.Products.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, CreateProductCommandResponse>
    {
        readonly IWriteRepository<Product> _writeRepository;
        readonly IMapper _mapper;

        public CreateProductCommandHandler(IWriteRepository<Product> writeRepository, IMapper mapper)
        {
            _writeRepository = writeRepository;
            _mapper = mapper;
        }

        public async Task<CreateProductCommandResponse> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            await _writeRepository.AddAsync(_mapper.Map<Product>(request.Product));
            await _writeRepository.SaveAsync();
            return new();
        }
    }
}
