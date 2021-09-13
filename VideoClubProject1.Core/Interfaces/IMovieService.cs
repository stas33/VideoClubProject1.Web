using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoClubProject1.Core.Entities;

namespace VideoClubProject1.Core.Interfaces
{
    public interface IMovieService
    {
        IEnumerable<Movie> GetAllMovies();

        Movie GetMovieById(int id);

        Movie GetMovieByTitle(string title);

        void AddMovie(Movie movie);

        void EditMovie(Movie movie);

        Movie GetMovieOfRental(History history);

        int GetMovieIdOfRental(History h);

        Movie GetMovieFromCopy(PhysicalCopy copy);

    }
}
