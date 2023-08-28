using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository.IRepository
{

    //when we working with generic we don't know what the class with be 
    public interface IRepository <T> where T : class
    {


        // T- Category
        IEnumerable<T> GetAll (Expression<Func<T, bool>>? filter = null,  string? includeProperties = null);
        T GetFirstOrDefault();
        T Get(Expression<Func<T, bool>> filter, string? includeProperties = null );
        void Add(T entity);
       // void Update(T entity);  

        void Remove(T entity);  
        void RemoveRange(IEnumerable<T> entities);  

    }
}
