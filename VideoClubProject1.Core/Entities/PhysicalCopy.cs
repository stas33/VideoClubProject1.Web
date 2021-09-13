using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoClubProject1.Core.Entities
{
    public class PhysicalCopy
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public bool Availability { get; set; }

        [DisplayName("Remaining Copies: ")]
        public int NumOfCopies { get; set; }

        public PhysicalCopy(int id, int movieid, bool availability)
        {
            Id = id;
            MovieId = movieid;
            Availability = availability; 
        }

        public PhysicalCopy()
        {

        }

    }
}
