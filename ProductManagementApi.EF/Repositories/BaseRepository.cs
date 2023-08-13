using Microsoft.EntityFrameworkCore;
using ProductManagementApi.Core.Interfaces;
using ProductManagementApi.Core.Models;
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

        public T DeleteProduct(T product)
        {
            context.Set<T>().Remove(product);
            context.SaveChanges();
            return product;
        }

        public async Task<T> FindProduct(int id)
        {
            return await context.Set<T>().FindAsync(id);
        }
        public IEnumerable<T> PipeAllProducts()
        {
            return context.Set<T>().ToList();
        }

        public T UpdateProduct(T product)
        {
            context.Set<T>().Update(product);
            context.SaveChanges();
            return product;
        }

    }
}
