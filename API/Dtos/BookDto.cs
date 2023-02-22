using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class BookDto
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Cover { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public string Genre { get; set; }
        [Required]
        public string Author { get; set; }
    }
}