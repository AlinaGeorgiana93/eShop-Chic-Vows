﻿namespace eShop.Data.Services;

public interface IDbService
{
    Task<List<TDto>> GetAsync<TEntity, TDto>()
        where TEntity : class
        where TDto : class;

    Task<TDto> SingleAsync<TEntity, TDto>(int id)
        where TEntity : class, IEntity where
        TDto : class;

    Task<TEntity> AddAsync<TEntity, TDto>(TDto dto)
        where TEntity : class where TDto : class;

    void Update<TEntity, TDto>(TDto dto)
        where TEntity : class, IEntity
        where TDto : class;

    Task<bool> DeleteAsync<TEntity>(int id) 
        where TEntity : class, IEntity;

    bool Delete<TEntity, TDto>(TDto dto)
        where TEntity : class where TDto : class;

    Task<bool> SaveChangesAsync();
}
