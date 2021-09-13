using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using VideoClubProject1.Core.Entities;
using VideoClubProject1.Core.Enumerations;

namespace VideoClubProject1.Web.Areas.Histories.Models
{
    public class UpdateRentalBindingModel
    {
        public ApplicationUser User { get; set; }

        public History history { get; set; }

        public Movie movie { get; set; }

        public int Id { get; set; }

        [DisplayName("Full Name")]
        public string RenterFullName { get; set; }

        [DisplayName("Rental Date")]
        public DateTime RentalDate { get; set; }

        [DisplayName("Expected Date")]
        public DateTime ExpectedDate { get; set; }

        [DisplayName("Return Date")]
        public ReturnType ReturnDate { get; set; }

        [DisplayName("Movie")]
        public Movie MovieId { get; set; }

        public int PhysicalCopyId { get; set; }

        public ApplicationUser UserId { get; set; }

        public UpdateRentalBindingModel(int id, string renterfname, DateTime rentdate, DateTime expdate,
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

    }
}