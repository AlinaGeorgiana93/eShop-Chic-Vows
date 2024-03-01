
using eShop.API.DTO.DTOs;
using eShop.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace eShop.Data.Services;

public class FilterDbService(EShopContext db, IMapper mapper) : DbService(db, mapper)
{
    public async Task<List<FilterTypeGetDTO>> GetProductsByCategoryAsync(int categoryId)
    {
        IncludeNavigationsFor<Category>();
        //IncludeNavigationsFor<Size>();
        var filterIds = GetAsync<CategoryFilter>(pc => pc.CategoryId.Equals(categoryId))
            .Select(pc => pc.FilterId);
        var filters = await GetAsync<Filter>(p => filterIds.Contains(p.Id)).ToListAsync();
        return MapList<Filter, FilterTypeGetDTO>(filters);
    }
    public List<TDto> MapList<TEntity, TDto>(List<TEntity> entities)
    where TEntity : class
    where TDto : class
    {
        return _mapper.Map<List<TDto>>(entities);
    }
}
