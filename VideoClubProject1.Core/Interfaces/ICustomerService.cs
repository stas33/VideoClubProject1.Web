using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoClubProject1.Core.Entities;

namespace VideoClubProject1.Core.Interfaces
{
    public interface ICustomerService
    {
        IEnumerable<ApplicationUser> GetCustomers();

        ApplicationUser GetCustomerByUsername(string username);

        ApplicationUser GetCustomerByFullName(string fname);

        //string GetCustomerByFullName2(string username);

        //ApplicationUser GetCustomerById(ApplicationUser usr);

        ApplicationUser GetCustomerOfRental(History history);

        ApplicationUser GetCust(History history);

        void IncreaseRentals(ApplicationUser usr);

        void ReduceRentals(History h);

        int GetCustomerId(string username);

        ApplicationUser GetCustomerByUsNameObject(History history);

    }
}
