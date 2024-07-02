using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCart.Helpers;
using XCart.Models.Product;

namespace XCart.Services.Product
{
    public class ProductService : IProductService
    {
        private readonly SQLiteAsyncConnection _database;

        public ProductService(DatabaseHelper database) => _database = database.GetDatabaseConnection();
        public Task AddProductAsync(ProductModel product)
        {
            return _database.InsertAsync(product);
        }

        public Task<List<ProductModel>> GetAllProductsAsync()
        {
            return _database.Table<ProductModel>().ToListAsync();
        }

        public Task UpdateProductAsync(ProductModel product)
        {
            return _database.UpdateAsync(product);
        }
    }
}
