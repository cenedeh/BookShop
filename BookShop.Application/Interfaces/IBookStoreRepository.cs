using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BookShop.Application.Interfaces
{
    public interface IBookStoreRepository
    {
        void Create<T>(T obj) where T : class;
        void Update<T>(T obj) where T : class;
        Task<IEnumerable<T>> FindByCondition<T>(Expression<Func<T, bool>> result) where T : class;
        IEnumerable<T> FindByCondition<T>(IEnumerable<T> source, Expression<Func<T, object>> orderByExpression) where T : class;
        IEnumerable<T> FindByCondition<T>(IEnumerable<T> source, Expression<Func<T, object>> orderByExpression, Expression<Func<T, string>> thenByExpression) where T : class;
        IEnumerable<T> FindByCondition<T>(Expression<Func<T, object>> orderByExpression, Expression<Func<T, string>> thenByExpression) where T : class;
        Task<T> FindSingleByCondition<T>(Expression<Func<T, bool>> expression) where T : class;
        Task<T> FindById<T>(int id) where T : class, new();
        IQueryable<T> GetAll<T>() where T : class;
        void DeleteAsync<T>(T obj) where T : class;
        Task<bool> SaveChanges();
    }
}
