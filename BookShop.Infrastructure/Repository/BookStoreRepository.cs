using BookShop.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BookShop.Infrastructure.Repository
{
    public class BookStoreRepository : IBookStoreRepository
    {
        private readonly BookStoreContext _bookStoreContext;

        public BookStoreRepository(BookStoreContext bookStoreContext)
        {
            _bookStoreContext = bookStoreContext;
        }
        public void Create<T>(T obj) where T : class
        {
            _bookStoreContext.Set<T>().Add(obj);
        }

        public void Update<T>(T obj) where T : class
        {
            _bookStoreContext.Set<T>().Update(obj);
        }

        public async Task<IEnumerable<T>> FindByCondition<T>(Expression<Func<T, bool>> expression) where T : class
        {
            var result = await _bookStoreContext.Set<T>().Where(expression).ToListAsync();
            return result;
        }
        
        public IEnumerable<T> FindByCondition<T>(Expression<Func<T, object>> orderByExpression, Expression<Func<T, string>> thenByExpression) where T : class
        {
            var result = _bookStoreContext.Set<T>().OrderBy(orderByExpression).ThenBy(thenByExpression);
            return result;
        }

        public IEnumerable<T> FindByCondition<T>(IEnumerable<T> source, Expression<Func<T, object>> orderByExpression, Expression<Func<T, string>> thenByExpression) where T : class
        {
            var result = source.AsQueryable().OrderBy(orderByExpression).ThenBy(thenByExpression);
            return result;
        }

        public IEnumerable<T> FindByCondition<T>(IEnumerable<T> source, Expression<Func<T, object>> orderByExpression) where T : class
        {
            var result = source.AsQueryable().OrderBy(orderByExpression);
            return result;
        }
        public async Task<T> FindSingleByCondition<T>(Expression<Func<T, bool>> expression) where T : class
        {
            var result = await _bookStoreContext.Set<T>().Where(expression).FirstOrDefaultAsync();
            return result;
        }
        public async Task<T> FindById<T>(int id) where T : class, new()
        {
            var response = await _bookStoreContext.Set<T>().FindAsync(id);
            return response;
        }

        public IQueryable<T> GetAll<T>() where T : class
        {
            return _bookStoreContext.Set<T>().AsNoTracking();
        }
        
        public void DeleteAsync<T>(T obj) where T : class
        {
            _bookStoreContext.Set<T>().Remove(obj);
        }
        public async Task<bool> SaveChanges()
        {
            using (var scope = await _bookStoreContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var obj = await _bookStoreContext.SaveChangesAsync();
                    if (obj > 0)
                    {
                        await scope.CommitAsync();
                        return true;
                    }
                }
                catch (Exception)
                {
                    await scope.RollbackAsync();
                    throw;
                }
                return false;
            }
        }
    }
}
