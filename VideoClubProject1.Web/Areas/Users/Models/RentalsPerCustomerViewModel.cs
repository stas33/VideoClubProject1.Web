using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VideoClubProject1.Core.Entities;

namespace VideoClubProject1.Web.Areas.Users.Models
{
    public class RentalsPerCustomerViewModel
    {
        public History history { get; set; }

        public IEnumerable<History> rentalsOfCustomer { get; set; }

        public RentalsPerCustomerViewModel(IEnumerable<History> rentals)
        {
            rentalsOfCustomer = rentals;
        }

        public RentalsPerCustomerViewModel()
        {

        }
    }
}