using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IBookRepository
    {
        Task<IReadOnlyList<Book>> GetBooksByOrderAsync(string order);
        Task<IReadOnlyList<Book>> GetBooksAsync();
        Task DeleteBookAsync(int id);
        Task<Book> CreateOrUpdateBookAsync(Book book);

        Task<IReadOnlyList<Book>> Top10BooksAsync();
        Task<IReadOnlyList<Book>> Top10BooksByGenreAsync(string genre);
        Task<Book> GetBookDetailAsync(int id);
    }
}