using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoClubProject1.Core.Entities;
using VideoClubProject1.Infrastructure.Models;
using static VideoClubProject1.Infrastructure.Services.MoviePagingService;

namespace VideoClubProject1.Infrastructure.Services
{
    public interface IMoviePagingService
    {

        Task<PaginationModel<Movie>> GetPaginatedMovies(int? movietype, string title, SortingOrder order, PaginationDto dto);
    }
}
