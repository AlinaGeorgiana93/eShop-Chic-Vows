﻿namespace eShop.Data.Entities;

public class FilterType : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public List<Filter> Filters { get; } = [];
}