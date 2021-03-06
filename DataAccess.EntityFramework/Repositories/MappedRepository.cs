﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using SocialNetwork.DataAccess.EntityFramework.Entities;

namespace SocialNetwork.DataAccess.EntityFramework.Repositories
{
    public abstract class MappedRepository<TSource, TDestination>
        where TSource : class, IEntity
    {
        protected DbContext context;
        protected DbSet<TSource> dataSet;
        protected IMapper mapper;
        
        public MappedRepository(DbContext context, IMapper mapper)
        {
            this.context = context;
            dataSet = context.Set<TSource>();
            this.mapper = mapper;
        }

        public IQueryable<TDestination> All
            => dataSet.UseAsDataSource(mapper).For<TDestination>();

        public IQueryable<TDestination> AllIncluding(params Expression<Func<TDestination, object>>[] includeProperties)
        {
            IQueryable<TDestination> query = dataSet.UseAsDataSource(mapper).For<TDestination>();
            foreach (var includeProperty in includeProperties)
                query = query.Include(includeProperty);

            return query;
        }

        public void Delete(int id)
        {
            var toDelete = dataSet.Find(id);
            dataSet.Remove(toDelete);
        }

        public TDestination Find(int id)
        {
            var entity = dataSet.Find(id);

            if (entity != null)
                return mapper.Map<TSource, TDestination>(entity);
            return default(TDestination);
        }

        public void InsertOrUpdate(TDestination model)
        {
            var entity = mapper.Map<TDestination, TSource>(model);

            if (entity.Id == default(int))
            {
                // New entity
                dataSet.Add(entity);
            }
            else
            {
                // Existing entity
                var existing = dataSet.Find(entity.Id);
                context.Entry(existing).State = EntityState.Detached;
                context.Entry(entity).State = EntityState.Modified;
            }

            Save();
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
