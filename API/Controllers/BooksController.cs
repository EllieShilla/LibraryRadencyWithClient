using API.Dtos;
using API.Errors;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BooksController : BaseController
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        private readonly IReviewRepository _reviewRepository;
        private readonly IRatingRepository _ratingRepository;
        private readonly IConfiguration _config;

        public BooksController(IBookRepository bookRepository, IReviewRepository reviewRepository, IRatingRepository ratingRepository,
        IMapper mapper, IConfiguration config)
        {
            _config = config;
            _ratingRepository = ratingRepository;
            _reviewRepository = reviewRepository;
            _mapper = mapper;
            _bookRepository = bookRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<BasicInfoAboutBookDto>>> GetBooks(string order)
        {
            var result = await _bookRepository.GetBooksByOrderAsync(order);
            var data = _mapper.Map<IReadOnlyList<Book>, IReadOnlyList<BasicInfoAboutBookDto>>(result);

            return Ok(data);
        }

        [HttpGet("all")]
        public async Task<ActionResult<IReadOnlyList<BasicInfoAboutBookDto>>> GetAllBooks()
        {
            var result = await _bookRepository.GetBooksAsync();
            var data = _mapper.Map<IReadOnlyList<Book>, IReadOnlyList<BasicInfoAboutBookDto>>(result);

            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BasicInfoAboutBookWithReviewsDto>> GetBookWithReviews(int id)
        {
            var result = await _bookRepository.GetBookDetailAsync(id);
            var data = _mapper.Map<Book, BasicInfoAboutBookWithReviewsDto>(result);

            return Ok(data);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBook(int id, string secret)
        {
            if (!_config["SecretKeyForDelete"].Equals(secret))
                return BadRequest("Entered the wrong key to delete");

            await _bookRepository.DeleteBookAsync(id);

            return Ok();
        }

        [HttpPost("save")]
        public async Task<ActionResult<int>> SaveOrUpdateBook(BookDto bookDto)
        {
            var book = _mapper.Map<BookDto, Book>(bookDto);
            var result = await _bookRepository.CreateOrUpdateBookAsync(book);

            return Ok(_mapper.Map<Book, IdDto>(result));
        }

        [HttpPut("{id}/review")]
        public async Task<ActionResult<int>> SaveReview(int id, SetReviewDto reviewDto)
        {
            var review = _mapper.Map<SetReviewDto, Review>(reviewDto);
            var result = await _reviewRepository.SaveReviewAsync(id, review);

            return Ok(_mapper.Map<Review, IdDto>(result));
        }

        [HttpPut("{id}/rate")]
        public async Task<ActionResult> SaveRate(int id, RateDto rateDto)
        {
            var rating = _mapper.Map<RateDto, Rating>(rateDto);
            var result = await _ratingRepository.SaveRateAsync(id, rating);

            return Ok();
        }
    }
}