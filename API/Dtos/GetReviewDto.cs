using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class GetReviewDto
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string Reviewer { get; set; }
    }
}