using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoClubProject1.Core.Entities;

namespace VideoClubProject1.Core.Interfaces
{
    public interface IHistoryService
    {
        IEnumerable<History> GetAllHistories();

        IEnumerable<History> GetActiveRentals();

        IEnumerable<History> GetRentalsPerCustomer(string id);

        History GetRentalById(int id);

        History GetRental(History history);

        History GetRentalByCustomerFnameAndId(ApplicationUser usr, int id);

        void AddRental(History history);

        void UpdateRentalReturnDate(History history);

        int GetMaxRentalId();

        void CreateRental(History history);
    }
}
