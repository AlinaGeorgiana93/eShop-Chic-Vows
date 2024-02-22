namespace eShop.API.DTO.DTOs;

public class ProductSizeDTOs
{
    public class ProductSizePostDTO
    {
        public int SizeId { get; set; }
        public int ProductId { get; set; }
    }
    public class ProductSizeDeleteDTO : ProductSizePostDTO
    {
    }
    public class ProductSizeGetDTO : ProductSizePostDTO
    {
    }

    public class ProductSizeSmallGetDTO
    {
        public int SizeId { get; set; }
    }
}
