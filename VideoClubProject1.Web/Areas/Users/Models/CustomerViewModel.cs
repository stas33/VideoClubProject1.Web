using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VideoClubProject1.Core.Entities;

namespace VideoClubProject1.Web.Areas.Users.Models
{
    public class CustomerViewModel
    {

        public ApplicationUser User { get; set; }
        public IEnumerable<ApplicationUser> customers { get; set; }

        public CustomerViewModel(string uname, string fname, int rentals)
        {
            User.UserName = uname;
            User.FullName = fname;
            User.Rentals = rentals;
        }

        public CustomerViewModel(IEnumerable<ApplicationUser> custs)
        {
            customers = custs;
        }
    }
}