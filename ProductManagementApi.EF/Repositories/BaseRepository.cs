using Microsoft.EntityFrameworkCore;
using ProductManagementApi.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementApi.EF.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly ApplicationDbContext context;

        public BaseRepository(ApplicationDbContext _context)
        {
            this.context = _context;
        }

        public T AddNewProduct(T product)
        {
            context.Set<T>().Add(product);
            context.SaveChanges();
            return product;
        }

        public T DeleteProduct(int id)
        {
            throw new NotImplementedException();
        }

       /* public T PipeAllProducts()
        {
            return context.Set<T>().Find(3);
        }*/

        public IEnumerable<T> PipeAllProducts()
        {
            return context.Set<T>().ToList();
        }
    }
}
