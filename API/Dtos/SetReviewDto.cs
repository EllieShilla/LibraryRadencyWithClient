using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class SetReviewDto
    {
        [Required]
        public string Message { get; set; }
        [Required]
        public string Reviewer { get; set; }
    }
}