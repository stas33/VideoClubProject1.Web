using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using VideoClubProject1.Core.Enumerations;

namespace VideoClubProject1.Core.Entities
{
    public class History
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("Full Name")]
        public string RenterFullName { get; set; }

        [Required]
        [DisplayName("Rental Date")]
        public DateTime RentalDate { get; set; }

        [Required]
        [DisplayName("Expected Date")]
        public DateTime ExpectedDate { get; set; }


        [DisplayName("Return Date")]
        public ReturnType ReturnDate { get; set; }

        [Required]
        [DisplayName("Movie")]
        public Movie MovieId { get; set; }

        public int PhysicalCopyId { get; set; }

        public ApplicationUser UserId { get; set; }

        public IEnumerable<SelectListItem> Titles { get; set; }

        public History(int id, string renterfname, DateTime rentdate, DateTime expdate,
            ReturnType retdate, int copyid, Movie mv, ApplicationUser usr)
        {
            Id = id;
            RenterFullName = renterfname;
            RentalDate = rentdate;
            ExpectedDate = expdate;
            ReturnDate = retdate;
            PhysicalCopyId = copyid;
            MovieId = mv;
            UserId = usr;
        }

        public History(int id, string renterfname, DateTime rentdate, DateTime expdate,
            ReturnType retdate, ApplicationUser usr, IEnumerable<SelectListItem> mvtitles)
        {
            Id = id;
            RenterFullName = renterfname;
            RentalDate = rentdate;
            ExpectedDate = expdate;
            ReturnDate = retdate;
            UserId = usr;
            Titles = mvtitles;
        }

        public History(int id, string renterfname, DateTime rentdate, DateTime expdate,
            ReturnType retdate, ApplicationUser usr, Movie mv, IEnumerable<SelectListItem> mvtitles)
        {
            Id = id;
            RenterFullName = renterfname;
            RentalDate = rentdate;
            ExpectedDate = expdate;
            ReturnDate = retdate;
            MovieId = mv;
            UserId = usr;
            Titles = mvtitles;
        }

        public History(int id, DateTime rentdate, DateTime expdate,
            ReturnType retdate, ApplicationUser usr, int copyid)
        {
            Id = id;
            RentalDate = rentdate;
            ExpectedDate = expdate;
            ReturnDate = retdate;
            UserId = usr;
            PhysicalCopyId = copyid;
        }

        public History()
        {

        }
    }
}
