﻿
using MongoDB.Driver;
using Shared.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common.Models
{
    public class PagedList<T> :List<T>
    {
        public PagedList(IEnumerable<T> items,long totalItems, int pageIndex, int pageSize)
        {
            //metadata tong quan hoa lan nua
            _metaData = new MetaData
            {
                TotalItems = totalItems,
                PageSize = pageSize,
                CurrentPage = pageIndex,
                //TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize)
            };
            AddRange(items);
        }


        private MetaData _metaData { get; }

        public MetaData GetMetaData() => _metaData;

        public static async Task<PagedList<T>> ToPagedList(IMongoCollection<T> source, 
            FilterDefinition<T> filter, int pageIndex, int pageSize)
        {
            var count = await source.Find(filter).CountDocumentsAsync();
            var items = await source.Find(filter).Skip((pageIndex - 1) * pageSize).Limit(pageSize)
                .ToListAsync();

            return new PagedList<T>(items, count, pageIndex,pageSize);
        }
    }
}
