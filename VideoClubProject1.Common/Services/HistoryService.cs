using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoClubProject1.Core.Entities;
using VideoClubProject1.Core.Enumerations;
using VideoClubProject1.Core.Interfaces;
using VideoClubProject1.Infrastructure.Data;


namespace VideoClubProject1.Common.Services
{
    public class HistoryService : IHistoryService
    {
        private ApplicationDbContext db;
        private readonly IMovieService dbmovie;
        private readonly ICopyService dbcopy;
        private readonly ICustomerService dbcust;

        public HistoryService(ApplicationDbContext dbcont, IMovieService db_movie, ICopyService db_copy, ICustomerService db_cust)
        {
            db = dbcont;
            dbmovie = db_movie;
            dbcopy = db_copy;
            dbcust = db_cust;
        }

        public IEnumerable<History> GetAllHistories()
        {
            return db.Histories.Where(r => r.UserId.UserName != "Admin").Include(r => r.MovieId).ToList();
        }

        public IEnumerable<History> GetActiveRentals()
        {
            return db.Histories.Where(r => r.ReturnDate != ReturnType.Scheduled).Include(r => r.MovieId).ToList();
        }

        public void AddRental(History history)
        {
            db.Histories.Add(history);
            history.Id = db.Histories.Max(r => r.Id) + 1;
            db.SaveChanges();
        }

        public History GetRentalById(int id)
        {
            return db.Histories.FirstOrDefault(r => r.Id == id);
        }

        public History GetRental(History history)
        {
            return db.Histories.FirstOrDefault(r => r.Id == history.Id);
        }

        public History GetRentalByCustomerFnameAndId(ApplicationUser usr, int id)
        {
            return db.Histories.FirstOrDefault(r => r.UserId.FullName == usr.FullName && r.Id == id);
        }

        public IEnumerable<History> GetRentalsPerCustomer(string id)
        {
            return db.Histories.Where(r => r.UserId.Id == id).Include(r => r.MovieId).ToList();
        }

        public void UpdateRentalReturnDate(History history)
        {
            var existing = GetRental(history);
            if (existing != null)
            {
                existing.MovieId = dbmovie.GetMovieOfRental(history);
                existing.UserId = dbcust.GetCust(history);
                existing.ReturnDate = history.ReturnDate;
                if (existing.ReturnDate.Equals(ReturnType.Scheduled))
                {
                    var ret_copy = dbcopy.GetCopyByRental(existing);
                    ret_copy.Availability = true;
                    dbcust.ReduceRentals(existing);
                }
                db.SaveChanges();
            }
        }

        public int GetMaxRentalId()
        {
            return db.Histories.Max(r => r.Id) + 1;
        }

        public void CreateRental(History h)
        {
            // case select movie
            if (h.UserId.UserName != null)
            {
                h.UserId = dbcust.GetCustomerByUsNameObject(h);
                h.RenterFullName = h.UserId.FullName;
                var copy = dbcopy.GetCopyByCopyId(h.PhysicalCopyId);
                copy.Availability = false;
                h.MovieId = dbmovie.GetMovieFromCopy(copy);
                dbcust.IncreaseRentals(h.UserId);
            }
            // case select user
            else
            {
                h.MovieId = dbmovie.GetMovieOfRental(h);
                h.UserId = dbcust.GetCustomerOfRental(h);              
                var copy = dbcopy.GetLastAvailableCopyOfRental(h);                
                copy.Availability = false;
                h.PhysicalCopyId = dbcopy.GetLastAvailaibleCopyIdOfRental(h);
                dbcust.IncreaseRentals(h.UserId);
            }
            AddRental(h);
        }
    }
}
