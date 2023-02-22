using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class RecommendedController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IBookRepository _bookRepository;
        public RecommendedController(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<BasicInfoAboutBookDto>>> GetBooksByGenre(string genre)
        {
            List<Book> books = new List<Book>();

            if (genre!=null)
                books.AddRange(await _bookRepository.Top10BooksByGenreAsync(genre));
            else
                books.AddRange(await _bookRepository.Top10BooksAsync());

            var data = _mapper.Map<IReadOnlyList<Book>, IReadOnlyList<BasicInfoAboutBookDto>>(books);

            return Ok(data);
        }
    }
}