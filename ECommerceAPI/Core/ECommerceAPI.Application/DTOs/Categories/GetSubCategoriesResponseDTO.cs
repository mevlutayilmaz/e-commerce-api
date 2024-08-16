namespace ECommerceAPI.Application.DTOs.Categories
{
    public class GetSubCategoriesResponseDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid? ParentCategoryId { get; set; }
        public ICollection<GetSubCategoriesResponseDTO> SubCategories { get; set; } = new HashSet<GetSubCategoriesResponseDTO>();
    }
}
