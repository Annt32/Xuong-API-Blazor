using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AppAPI.Repositories
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        IQueryable<T> AsQueryable();  // Trả về IQueryable cho các truy vấn linh hoạt
        T GetById(Guid id);
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate); // Tìm đối tượng theo điều kiện
        void Add(T entity);
        void AddRange(IEnumerable<T> entities); // Thêm nhiều đối tượng
        void Update(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities); // Xóa nhiều đối tượng
    }
}
