using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using Shared.SeedWork;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Infrastructure.Common.Models;

public class PagedList<T> : List<T>
{
    public PagedList(IEnumerable<T> items, long totalItems, int pageIndex, int pageSize)
    {
        _metaData = new MetaData
        {
            TotalItems = totalItems,
            PageSize = pageSize,
            CurrentPage = pageIndex,
            TotalPages = (int) Math.Ceiling(totalItems / (double) pageSize)
        };
        AddRange(items);
    }

    private MetaData _metaData { get; }

    public MetaData GetMetaData()
    {
        return _metaData;
    }

    public static async Task<PagedList<T>> ToPagedList(IQueryable<T> source, string? sortField, int? sortOrder,  int pageNumber, int pageSize)
    {
        // SORT
        if (!string.IsNullOrEmpty(sortField))
        {
            switch (sortOrder)
            {
                case 1:
                    source = source.OrderBy(x => EF.Property<object>(x, sortField));
                    break;
                case -1:
                    source = source.OrderByDescending(x => EF.Property<object>(x, sortField));
                    break;
                default:
                    break;
            }
        }
        var count = await source.CountAsync();
        var items = await source
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize).ToListAsync();

        return new PagedList<T>(items, count, pageNumber, pageSize);
    }
    
    public static async Task<PagedList<T>> ToPagedList(IMongoCollection<T> source, FilterDefinition<T> filter, int pageIndex, int pageSize)
    {
        var count = await source.Find(filter).CountDocumentsAsync();
        var items = await source.Find(filter)
            .Skip((pageIndex - 1) * pageSize)
            .Limit(pageSize)
            .ToListAsync();

        return new PagedList<T>(items, count, pageIndex, pageSize);
    }
}