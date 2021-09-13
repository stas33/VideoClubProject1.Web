using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VideoClubProject1.Core.Entities;
using VideoClubProject1.Core.Enumerations;

namespace VideoClubProject1.Web.Areas.Histories.Models
{
    public class RentalViewModel
    {

        public History history { get; set; }
        public IEnumerable<History> rentalslist { get; set; }

        public RentalViewModel(string renterfname, DateTime rentdate, DateTime expdate, ReturnType returndate, Movie mv, int copyid)
        {
            history.RenterFullName = renterfname;
            history.RentalDate = rentdate;
            history.ExpectedDate = expdate;
            history.ReturnDate = returndate;
            history.MovieId = mv;
            history.PhysicalCopyId = copyid;
        }

        public RentalViewModel(IEnumerable<History> rentals)
        {
            rentalslist = rentals;
        }
    }
}