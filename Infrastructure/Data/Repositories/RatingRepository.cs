using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{
    public class RatingRepository : IRatingRepository
    {
        private readonly LibraryContext _context;
        public RatingRepository(LibraryContext context)
        {
            _context = context;
        }

        public async Task<Rating> SaveRateAsync(int bookId, Rating rating)
        {
            var book = await _context.Books.FirstOrDefaultAsync(book => book.Id == bookId);

            rating.BookId = bookId;
            await _context.Ratings.AddAsync(rating);
            await _context.SaveChangesAsync();
            return rating;
        }
    }
}