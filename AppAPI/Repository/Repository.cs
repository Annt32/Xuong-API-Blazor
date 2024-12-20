﻿using AppData.AppDbContext;
using AppData.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AppAPI.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly AppDBContext _context;
        private readonly DbSet<T> _entities;

        public Repository(AppDBContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            if (typeof(T) == typeof(Field))
            {
                return _context.Set<Field>().Include(f => f.FieldType).ToList() as IEnumerable<T>;
            }
            return _entities.ToList();
        }

        public IQueryable<T> AsQueryable()
        {
            return _entities.AsQueryable();
        }

        public T GetById(Guid id)
        {
            return _entities.Find(id);
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _entities.Where(predicate).ToList();
        }

        public void Add(T entity)
        {
            _entities.Add(entity);
            _context.SaveChanges();
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _entities.AddRange(entities);
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            _entities.Update(entity);
            _context.SaveChanges();
        }

        public void Remove(T entity)
        {
            _entities.Remove(entity);
            _context.SaveChanges();
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _entities.RemoveRange(entities);
            _context.SaveChanges();
        }
        public async Task ModifileUpdate(T value) { _context.Entry(value).State = EntityState.Modified; _context.SaveChanges(); }

		public IQueryable<T> GetAllWithIncludes(params Expression<Func<T, object>>[] includes)
		{
			IQueryable<T> query = _entities;
			foreach (var include in includes)
			{
				query = query.Include(include);
			}
			return query.AsNoTracking();
		}

        public IEnumerable<Notification> GetNotificationsWithDetails()
        {
            if (typeof(T) == typeof(Notification))
            {
                return _context.Set<Notification>()
                    .Include(n => n.FieldShift)                // Bao gồm FieldShift
                    .ThenInclude(fs => fs.Field)              // Bao gồm Field trong FieldShift
                    .Include(n => n.FieldShift.Shift)         // Bao gồm Shift trong FieldShift
                    .ToList() as IEnumerable<Notification>;
            }
            throw new InvalidOperationException("This method is only for Notification entity.");
        }

    }
}
