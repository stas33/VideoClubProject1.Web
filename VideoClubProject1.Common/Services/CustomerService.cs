using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoClubProject1.Core.Entities;
//using VideoClubProject1.Core.Entities;
using VideoClubProject1.Core.Interfaces;
using VideoClubProject1.Infrastructure.Data;

namespace VideoClubProject1.Common.Services
{
    public class CustomerService : ICustomerService
    {

        private ApplicationDbContext db;

        public CustomerService(ApplicationDbContext dbcont)
        {
            db = dbcont;
        }

        public IEnumerable<ApplicationUser> GetCustomers()
        {
            return db.Users.AsNoTracking().Where(r => r.UserName != "Admin");

        }

        public ApplicationUser GetCustomerByUsername(string username)
        {
            return db.Users.FirstOrDefault(r => r.UserName == username);
        }

        public ApplicationUser GetCustomerByFullName(string fname)
        {
            return db.Users.FirstOrDefault(r => r.FullName == fname);
        }

        /*public string GetCustomerByFullName2(string username)
        {
            return db.Users.FirstOrDefault(r => r.UserName == username).FullName;
        }*/

        public ApplicationUser GetCustomerOfRental(History history)
        {
            return db.Users.FirstOrDefault(r => r.Id == history.UserId.Id);
        }

        public ApplicationUser GetCust(History history)
        {
            return db.Users.FirstOrDefault(r => r.FullName == history.RenterFullName);
        }

        public ApplicationUser GetCustomerByUsNameObject(History h)
        {
            return db.Users.FirstOrDefault(r => r.UserName == h.UserId.UserName);
        }

        public void IncreaseRentals(ApplicationUser usr)
        {
            usr.Rentals = db.Users.Max(r => r.Rentals) + 1;
            //db.SaveChanges();
        }

        /*public ApplicationUser GetCustomerById(ApplicationUser usr)
        {
            return db.Users.FirstOrDefault(r => r.Id == usr.Id);
        }*/

        public void ReduceRentals(History h)
        {
            h.UserId.Rentals = db.Users.Max(r => r.Rentals) - 1;
        }

        public int GetCustomerId(string username)
        {
            return db.Users.Where(r => r.UserName == username).Count();
        }

    }
}
