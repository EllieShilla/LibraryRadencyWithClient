using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class RateDto
    {
        [Required]
        [Range(1, 5, ErrorMessage = "Score can be from 1 to 5")]
        public int Score { get; set; }
    }
}