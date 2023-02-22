using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
    public class LibraryContextSeed
    {
        public static async Task SeedAsync(LibraryContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if (!context.Books.Any())
                {
                    var bookData = File.ReadAllText("../Infrastructure/Data/SeedData/books.json");
                    var books = JsonSerializer.Deserialize<List<Book>>(bookData);

                    foreach (var item in books)
                    {
                        context.Books.Add(item);
                    }

                    await context.SaveChangesAsync();
                }

                if (!context.Ratings.Any())
                {
                    var ratingData = File.ReadAllText("../Infrastructure/Data/SeedData/ratings.json");
                    var ratings = JsonSerializer.Deserialize<List<Rating>>(ratingData);

                    foreach (var item in ratings)
                    {
                        context.Ratings.Add(item);
                    }

                    await context.SaveChangesAsync();
                }

                if (!context.Reviews.Any())
                {
                    var reviewData = File.ReadAllText("../Infrastructure/Data/SeedData/reviews.json");
                    var rewievs = JsonSerializer.Deserialize<List<Review>>(reviewData);

                    foreach (var item in rewievs)
                    {
                        context.Reviews.Add(item);
                    }

                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<LibraryContextSeed>();
                logger.LogError(ex.Message);
            }
        }
    }
}