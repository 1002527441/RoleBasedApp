using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp15.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();

        void Add(T entity);

        void Delete(T entity);

        T FindById(object id);
       

        void DeleteAll(IEnumerable<T> entity);

        void Update(T entity);

        bool Any();
    }
}
