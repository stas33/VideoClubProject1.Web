using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace VideoClubProject1.Web.Areas.Copies.Models
{
    public class CopyAvailabilityViewModel
    {

        public bool Availability { get; set; }

        [DisplayName("Remaining Copies: ")]
        public int NumOfCopies { get; set; }

        public CopyAvailabilityViewModel(bool avail, int copiesno)
        {
            Availability = avail;
            NumOfCopies = copiesno;
        }
    }
}