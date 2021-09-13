using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoClubProject1.Core.Entities;
using VideoClubProject1.Core.Interfaces;
using VideoClubProject1.Infrastructure.Data;

namespace VideoClubProject1.Common.Services
{
    public class MovieService : IMovieService
    {

        private ApplicationDbContext db;

        public MovieService(ApplicationDbContext dbcont)
        {
            db = dbcont;
        }

        public IEnumerable<Movie> GetAllMovies()
        {
            return db.Movies.AsNoTracking();
        }

        public void AddMovie(Movie movie)
        {
            db.Movies.Add(movie);
            movie.Id = db.Movies.Max(r => r.Id) + 1;
            db.SaveChanges();
        }

        public Movie GetMovieById(int id)
        {
            return db.Movies.FirstOrDefault(r => r.Id == id);
        }

        public Movie GetMovieByTitle(string title)
        {
            return db.Movies.FirstOrDefault(r => r.Title == title);
        }

        public void EditMovie(Movie movie)
        {
            var existing = GetMovieById(movie.Id);
            if (existing != null)
            {
                existing.Title = movie.Title;
                existing.Description = movie.Description;
                existing.Type = movie.Type;
                db.SaveChanges();
            }
        }

        public Movie GetMovieOfRental(History history)
        {
            return db.Movies.FirstOrDefault(r => r.Id == history.MovieId.Id);
        }

        public int GetMovieIdOfRental(History h)
        {
            return db.PhysicalCopies.FirstOrDefault(r => r.Id == h.PhysicalCopyId).MovieId;
        }

        public Movie GetMovieFromCopy(PhysicalCopy copy)
        {
            return db.Movies.FirstOrDefault(r => r.Id == copy.MovieId);
        }

    }
}
