using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryContext _context;
        public BookRepository(LibraryContext context)
        {
            _context = context;
        }

        public async Task<Book> CreateBookAsync(Book book)
        {
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
            return book;
        }

        public async Task<Book> UpdateBookAsync(Book book)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
            return book;
        }

        public async Task<Book> CreateOrUpdateBookAsync(Book book)
        {
            if (book.Id == 0)
            {
                return await CreateBookAsync(book);
            }
            else
            {
                return await UpdateBookAsync(book);
            }
        }

        public async Task DeleteBookAsync(int id)
        {
            var book = await _context.Books.FirstOrDefaultAsync(book => book.Id == id);
            _context.Remove(book);
            await _context.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<Book>> GetBooksByOrderAsync(string order)
        {
            var result = await _context.Books
            .Where(book => book.Title.Equals(order) || book.Author.Equals(order))
            .Include(r => r.Reviews)
            .Include(r => r.Ratings)
           .ToListAsync();

            return result;
        }

        public async Task<IReadOnlyList<Book>> Top10BooksAsync()
        {
            var result = await _context.Books
            .Where(i => i.Reviews.Count() > 10)
            .Include(r => r.Reviews)
            .Include(r => r.Ratings)
            .OrderByDescending(i => i.Ratings.Average(s => s.Score))
            .Take(10)
           .ToListAsync();

            return result;
        }

        public async Task<IReadOnlyList<Book>> Top10BooksByGenreAsync(string genre)
        {
            var result = await _context.Books
            .Where(i => i.Reviews.Count() > 10 && i.Genre.ToLower().Equals(genre.ToLower()))
            .Include(r => r.Reviews)
            .Include(r => r.Ratings)
            .OrderByDescending(i => i.Ratings.Average(s => s.Score))
            .Take(10)
           .ToListAsync();

            return result;
        }

        public async Task<Book> GetBookDetailAsync(int id)
        {
            var result = await _context.Books
            .Include(r => r.Reviews)
            .Include(r => r.Ratings)
            .FirstOrDefaultAsync(book => book.Id == id);

            return result;
        }

        public async Task<IReadOnlyList<Book>> GetBooksAsync()
        {
            return await _context.Books
            .Include(r => r.Reviews)
            .Include(r => r.Ratings)
            .ToListAsync();
        }
    }
}