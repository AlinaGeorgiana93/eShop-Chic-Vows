﻿namespace eShop.API.DTO;

public class ProductPostDTO
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public string PictureURL { get; set; } = string.Empty;
    public decimal Price { get; set; }
}
public class ProductPutDTO : ProductPostDTO
{
    public int Id { get; set; }
}
public class ProductGetDTO : ProductPutDTO
{
    public List<ColorGetDTO>? Colors { get; set; }
    public List<SizeGetDTO>? Sizes { get; set; }
    //public List<FilterGetDTO>? Filters { get; set; }
}
public class CartItemDTO : ProductPutDTO
{
    public ColorGetDTO? Color { get; set; }
    public SizeGetDTO? Size { get; set; }
    
}

