using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCart.Models.Product;

namespace XCart.Services.Product
{
    public   interface IProductService
    {
        Task<List<ProductModel>> GetAllProductsAsync();
        Task AddProductAsync(ProductModel product);
        Task UpdateProductAsync(ProductModel product);
    }
}
