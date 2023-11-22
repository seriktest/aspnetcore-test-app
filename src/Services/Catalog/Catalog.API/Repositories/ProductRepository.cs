using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext _context;

        public ProductRepository(ICatalogContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _context.Products.Find(_ => true).ToListAsync().ConfigureAwait(false);
        }

        public async Task<Product> GetProductAsync(string id)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Id, id);
            return await _context.Products.Find(filter).FirstOrDefaultAsync().ConfigureAwait(false);
        }

        public async Task<IEnumerable<Product>> GetProductByNameAsync(string name)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Name, name);
            return await _context.Products.Find(filter).ToListAsync().ConfigureAwait(false);
        }

        public async Task<IEnumerable<Product>> GetProductByCategoryAsync(string category)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Category, category);
            return await _context.Products.Find(filter).ToListAsync().ConfigureAwait(false);
        }

        public async Task CreateProductAsync(Product product)
        {
            await _context.Products.InsertOneAsync(product).ConfigureAwait(false);
        }

        public async Task<bool> UpdateProductAsync(Product product)
        {
            var updateResult = await _context.Products.ReplaceOneAsync(filter: g => g.Id == product.Id, replacement: product).ConfigureAwait(false);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }

        public async Task<bool> DeleteProductAsync(string id)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Id, id);
            var deleteResult = await _context.Products.DeleteOneAsync(filter).ConfigureAwait(false);
            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }
    }
}