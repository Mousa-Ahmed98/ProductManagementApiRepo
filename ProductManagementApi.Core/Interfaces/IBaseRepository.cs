using ProductManagementApi.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementApi.Core.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        //T PipeAllProducts();
        IEnumerable<T> PipeAllProducts();

        T AddNewProduct(T product);

        T DeleteProduct(T product);

        Task<T> FindProduct(int id);

        T UpdateProduct(T product);
    }
}
