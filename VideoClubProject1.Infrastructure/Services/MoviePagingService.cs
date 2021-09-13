using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using VideoClubProject1.Core.Entities;
using VideoClubProject1.Infrastructure.Data;
using VideoClubProject1.Infrastructure.Models;


namespace VideoClubProject1.Infrastructure.Services
{
    public class MoviePagingService : IMoviePagingService
    {
        private ApplicationDbContext db;

        public MoviePagingService(ApplicationDbContext dbcontext)
        {
            db = dbcontext;
        }

        public enum SortingOrder
        {
            TitleAscending,
            TitleDescending
        }

        public async Task<PaginationModel<Movie>> GetPaginatedMovies(int? movieType, string title, SortingOrder order, PaginationDto dto)
        {
            var moviesQuery = db.Movies.OrderBy(m => m.Title).AsQueryable();

            if (!String.IsNullOrEmpty(title) && movieType != null)
            {
                moviesQuery = moviesQuery.Where(s =>
                    ((CultureInfo.InvariantCulture.CompareInfo.IndexOf(
                        s.Title,
                        title,
                        CompareOptions.IgnoreCase) >= 0)
                        && (int?)s.Type == movieType)
                    || ((CultureInfo.InvariantCulture.CompareInfo.IndexOf(
                        s.Description,
                        title,
                        CompareOptions.IgnoreCase) >= 0)
                        && (int?)s.Type == movieType)
                    );
            }
            else if (String.IsNullOrEmpty(title) && movieType != null)
            {
                moviesQuery = moviesQuery.Where(s => (int?)s.Type == movieType);
            }
            else if (!String.IsNullOrEmpty(title) && movieType == null)
            {
                moviesQuery = moviesQuery.Where(m => m.Title.Contains(title));
            }

            switch (order)
            {
                case SortingOrder.TitleAscending:
                    moviesQuery = moviesQuery.OrderBy(m => m.Title);
                    break;
                case SortingOrder.TitleDescending:
                    moviesQuery = moviesQuery.OrderByDescending(m => m.Title);
                    break;
                default:
                    moviesQuery = moviesQuery.OrderBy(m => m.Title);
                    break;
            }

            var moviesCount = await moviesQuery.CountAsync();
            var toSkip = (dto.CurrentPage - 1) * dto.PageSize;
            moviesQuery = moviesQuery.Skip(toSkip).Take(dto.PageSize);

            var movies = await moviesQuery.ToListAsync();

            var pagecount = (int)Math.Ceiling((double)moviesCount / dto.PageSize);

            var model = new PaginationModel<Movie>(movies, dto.CurrentPage, dto.PageSize, moviesCount, pagecount);

            return (model);
        }
    }
}
