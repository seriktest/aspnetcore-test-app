using Microsoft.Extensions.Logging;
using Ordering.Domain.Entities;

namespace Ordering.Infrastructure.Persistence; 

public class OrderContextSeed {
     public static async Task SeedAsync(OrderContext orderContext, ILogger<OrderContextSeed> logger) {
          if (!orderContext.Orders.Any()) {
               orderContext.Orders.AddRange(GetPreconfiguredOrders());
               await orderContext.SaveChangesAsync();
               logger.LogInformation("Seed database associated with context {DbContextName}", typeof(OrderContext).Name);
          }
     }
     
     private static IEnumerable<Order> GetPreconfiguredOrders() {
          return new List<Order>() {
               new Order() {
                    UserName = "swn",
                    TotalPrice = 350,
                    FirstName = "Sergio",
                    LastName = "Nunez",
                    EmailAddress = "JpjGZ@example.com",
                    AddressLine = "Calle 1",
                    Country = "Colombia",
                    State = "Antioquia",
                    ZipCode = "12345"
               }
          };
     }
}