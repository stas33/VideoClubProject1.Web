using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VideoClubProject1.Core.Entities;
using VideoClubProject1.Core.Enumerations;

namespace VideoClubProject1.Web.Areas.Histories.Models
{
    public class CreateRentalBindingModel
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

        public IEnumerable<SelectListItem> Titles { get; set; }

        public CreateRentalBindingModel(int id, string renterfname, DateTime rentdate, DateTime expdate,
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

        public CreateRentalBindingModel(int id, string renterfname, DateTime rentdate, DateTime expdate,
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

        public CreateRentalBindingModel(int id, DateTime rentdate, DateTime expdate,
            ReturnType retdate, Movie mv, int copyid)
        {
            Id = id;
            RentalDate = rentdate;
            ExpectedDate = expdate;
            ReturnDate = retdate;
            MovieId = mv;
            PhysicalCopyId = copyid;
        }

        public CreateRentalBindingModel()
        {

        }

    }
}