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
    public class CopyService : ICopyService
    {

        private ApplicationDbContext db;

        public CopyService(ApplicationDbContext dbcont)
        {
            db = dbcont;
        }

        public int GetNumberOfAvailableCopies(int id)
        {
            return db.PhysicalCopies.Count(c => c.MovieId == id && c.Availability == true);
        }

        public string GetAvailableCopyOfMovie(int id)
        {
            return "select * from dbo.PhysicalCopies where " + id + " in (select dbo.PhysicalCopies.MovieId from dbo.PhysicalCopies) and (Availability = 1)";
        }

        public PhysicalCopy GetFirstAvailableCopy(string query)
        {
            return db.PhysicalCopies.SqlQuery(query).FirstOrDefault();
        }

        public PhysicalCopy GetLastAvailableCopyOfRental(History history)
        {
            return db.PhysicalCopies.Where(r => r.MovieId == history.MovieId.Id && r.Availability == true).ToList().LastOrDefault();
        }

        public int GetLastAvailaibleCopyIdOfRental(History history)
        {
            return db.PhysicalCopies.Where(r => r.MovieId == history.MovieId.Id && r.Availability == true).Max(r => r.Id);
        }

        public int GetLastAvailableCopyIdByMovieTitle(Movie m)
        {
            return db.PhysicalCopies.Where(r => r.MovieId == m.Id && r.Availability == true).Max(r => r.Id);
        }

        public PhysicalCopy GetCopyByRental(History h)
        {
            return db.PhysicalCopies.FirstOrDefault(r => r.Id == h.PhysicalCopyId);
        }

        public PhysicalCopy GetCopyByCopyId(int id)
        {
            return db.PhysicalCopies.FirstOrDefault(r => r.Id == id);
        }

        public void AddCopy(Movie movie)
        {
            var copy1 = new PhysicalCopy() { Id = db.PhysicalCopies.Max(r => r.Id) + 1, MovieId = movie.Id, Availability = true };
            var copy2 = new PhysicalCopy() { Id = db.PhysicalCopies.Max(r => r.Id) + 1, MovieId = movie.Id, Availability = true };
            var copy3 = new PhysicalCopy() { Id = db.PhysicalCopies.Max(r => r.Id) + 1, MovieId = movie.Id, Availability = true };
            db.PhysicalCopies.Add(copy1);
            db.PhysicalCopies.Add(copy2);
            db.PhysicalCopies.Add(copy3);
            db.SaveChanges();
        }

    }
}
