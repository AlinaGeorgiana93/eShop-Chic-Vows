namespace eShop.API.DTO.DTOs;

public class FilterOptionPostDTO
{
    public int FilterId { get; set; }
    public int OptionId { get; set; }
}
public class FilterOptionDeleteDTO : FilterOptionPostDTO
{
}
