using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IRatingRepository
    {
        public Task<Rating> SaveRateAsync(int bookId, Rating rating);
    }
}