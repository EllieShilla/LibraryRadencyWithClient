using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly LibraryContext _context;
        public ReviewRepository(LibraryContext context)
        {
            _context = context;
        }

        public async Task<Review> SaveReviewAsync(int bookId, Review review)
        {
            var book = await _context.Books.FirstOrDefaultAsync(book => book.Id == bookId);

            review.BookId = bookId;
            await _context.Reviews.AddAsync(review);
            await _context.SaveChangesAsync();
            return review;
        }
    }
}