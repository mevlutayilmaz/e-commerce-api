using AutoMapper;
using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Commands.Categories.CreateCategory
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommandRequest, CreateCategoryCommandResponse>
    {
        readonly IWriteRepository<Category> _writeRepository;
        readonly IMapper _mapper;

        public CreateCategoryCommandHandler(IWriteRepository<Category> writeRepository, IMapper mapper)
        {
            _writeRepository = writeRepository;
            _mapper = mapper;
        }

        public async Task<CreateCategoryCommandResponse> Handle(CreateCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            var category = _mapper.Map<Category>(request);
            await _writeRepository.AddAsync(category);
            await _writeRepository.SaveAsync();
            return new()
            {
                Id = category.Id,
            };
        }
    }
}
