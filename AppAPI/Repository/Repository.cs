using AppData.AppDbContext;
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
        private readonly DbSet<T> _entities; // DbSet tương ứng với đối tượng T

        public Repository(AppDBContext context) 
        {
            _context = context;
            _entities = context.Set<T>(); // Gán DbSet cho đối tượng T
        }

        public IEnumerable<T> GetAll()
        {
            return _entities.ToList();
        }

        // Trả về IQueryable để có thể linh hoạt truy vấn
        public IQueryable<T> AsQueryable()
        {
            return _entities.AsQueryable();
        }

        public T GetById(Guid id)
        {
            return _entities.Find(id);
        }

        // Tìm các đối tượng theo điều kiện (predicate)
        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _entities.Where(predicate).ToList();
        }

        public void Add(T entity)
        {
            _entities.Add(entity);
            _context.SaveChanges();
        }

        // Thêm nhiều đối tượng cùng một lúc
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
    }
}
