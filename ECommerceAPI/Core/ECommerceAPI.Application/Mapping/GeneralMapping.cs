using AutoMapper;
using ECommerceAPI.Application.DTOs.Categories;
using ECommerceAPI.Application.DTOs.Prodcuts;
using ECommerceAPI.Application.DTOs.Users;
using ECommerceAPI.Application.Features.Commands.AppUsers.CreateUser;
using ECommerceAPI.Application.Features.Commands.Categories.CreateCategory;
using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Domain.Entities.Identity;

namespace ECommerceAPI.Application.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<AppUser, CreateUserCommandRequest>()
                .ReverseMap();
            CreateMap<CreateUserDTO, CreateUserCommandRequest>()
                .ReverseMap();
            CreateMap<CreateUserResponseDTO, CreateUserCommandResponse>()
                .ReverseMap();
            CreateMap<CreateProductDTO, Product>()
                .ReverseMap();
            CreateMap<Category, CreateCategoryCommandRequest>()
                .ReverseMap();
            CreateMap<Category, GetRootCategoriesResponseDTO>()
                .ReverseMap();
            CreateMap<Category, GetSubCategoriesResponseDTO>()
                .ReverseMap();
        }
    }
}
