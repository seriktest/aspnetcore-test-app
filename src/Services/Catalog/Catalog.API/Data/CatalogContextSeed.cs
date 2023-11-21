using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Data;

public static class CatalogContextSeed
{
    public static void SeedData(IMongoCollection<Product> products)
    {
        var existProduct = products.Find(p => true).Any();
        if (!existProduct)
        {
            products.InsertManyAsync(GetPreconfiguredProducts());
        }

    }

    private static IEnumerable<Product> GetPreconfiguredProducts()
    {
        return new List<Product>()
        {
            new Product()
            {
                Id = "602d2149e773f2a3990b47f5",
                Name = "IPhone X",
                Summary = "This is the summary of IPhone X",
                Description = "This is the description of IPhone X",
                ImageFile = "product-1.png",
                Price = 950.00M,
                Category = "Smart Phone"
            },
            new Product()
            {
                Id = "602d2149e773f2a3990b47f6",
                Name = "Samsung 10",
                Summary = "This is the summary of Samsung 10",
                Description = "This is the description of Samsung 10",
                ImageFile = "product-2.png",
                Price = 840.00M,
                Category = "Smart Phone"
            }
        };
    }
}