using AutoMapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VideoClubProject1.Core.Entities;
using VideoClubProject1.Core.Enumerations;
using VideoClubProject1.Core.Interfaces;
using VideoClubProject1.Infrastructure.Services;
using VideoClubProject1.Web.Areas.Histories.Models;
using VideoClubProject1.Web.Areas.Users.Models;
using VideoClubProject1.Web.Mappings;

namespace VideoClubProject1.Web.Areas.Histories.Controllers
{
    public class HistoriesController : Controller
    {
        private readonly ICustomerService dbcust;
        private readonly IMovieService dbmovie;
        private readonly ICopyService dbcopy;
        private readonly IHistoryService dbhistory;
        private readonly ILoggingService logger;
        private IMapper mapper => MapperInit.Init();

        public HistoriesController(ICustomerService db_cust, IMovieService db_movie, ICopyService db_copy, IHistoryService db_history, ILoggingService log)
        {
            dbcust = db_cust;
            dbmovie = db_movie;
            dbcopy = db_copy;
            dbhistory = db_history;
            logger = log;
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var getrentals = dbhistory.GetAllHistories();
            var rentalslist = new RentalViewModel(getrentals);
            logger.Writer.Information("Display of the rentals list...");

            return View(rentalslist);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ActiveRentalsList()
        {
            var activerentals = dbhistory.GetActiveRentals();
            var model = new RentalViewModel(activerentals);
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult CreateRental(string title, string username, CreateRentalBindingModel rentalForm)
        {
            //var rentalForm = new CreateRentalBindingModel();        
            if (username == null && title == null)
            {
                return View(rentalForm);
            }
            else if (username != null)  // case: select a user and then movie from dropdown
            {
                var usr = dbcust.GetCustomerByUsername(username);
                dbcust.IncreaseRentals(usr);
                rentalForm = new CreateRentalBindingModel(dbhistory.GetMaxRentalId(), usr.FullName, DateTime.Now, DateTime.Now.AddDays(7),
                    ReturnType.InProgess, usr, new SelectList(dbmovie.GetAllMovies(), "Id", "Title", 1));                
            }
            // case: select a movie and then type username
            else if (title != null)
            {
                Movie movieid = dbmovie.GetMovieByTitle(title);
                var copy = dbcopy.GetLastAvailableCopyIdByMovieTitle(movieid);
                if (copy <= 0)
                {
                    return View("CopyNotFound");
                }
                rentalForm = new CreateRentalBindingModel(dbhistory.GetMaxRentalId(), DateTime.Now, DateTime.Now.AddDays(7), ReturnType.InProgess,
                    movieid, copy);
            }
            return View(rentalForm);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateRental(CreateRentalBindingModel rentalForm)
        {
            // case: select a movie and then type username
            if (rentalForm.UserId.UserName != null)
            {
                rentalForm.Titles = new SelectList(dbmovie.GetAllMovies(), "Id", "Title", 1);
                string username = rentalForm.UserId.UserName;
                if (dbcust.GetCustomerId(username) <= 0)
                {
                    return View("CustomerNotFound");
                }
                //var history = new History(rentalForm.Id, rentalForm.RentalDate, rentalForm.ExpectedDate, rentalForm.ReturnDate,
                //    rentalForm.UserId, rentalForm.PhysicalCopyId);
                var history = mapper.Map<History>(rentalForm);
                dbhistory.CreateRental(history);
                logger.Writer.Information("A new rental just created successfully!");

                return RedirectToAction("Index", "Histories", new { Area = "Histories" });
            }
            // case: select a user and then movie from dropdown
            else
            {
                rentalForm.Titles = new SelectList(dbmovie.GetAllMovies(), "Id", "Title", 1);
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                if (!ModelState.IsValid)
                {
                    rentalForm.Titles = new SelectList(dbmovie.GetAllMovies(), "Id", "Title");
                    return View(new CreateRentalBindingModel());
                }
                try
                {
                    //var history = new History(rentalForm.Id, rentalForm.RenterFullName, rentalForm.RentalDate, rentalForm.ExpectedDate, rentalForm.ReturnDate,
                    //    rentalForm.UserId, rentalForm.MovieId, rentalForm.Titles);
                    var history = mapper.Map<History>(rentalForm);  
                    dbhistory.CreateRental(history);
                    logger.Writer.Information("A new rental just created successfully!");

                    return RedirectToAction("Index", "Histories", new { Area = "Histories" });
                }
                catch
                {
                    //rentalForm.history.Titles = new SelectList(dbmovie.GetAllMovies(), "Id", "Title");
                    return View(new CreateRentalBindingModel());
                }
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public JsonResult UpdateDateStatus(int id, string fname)
        {
            var usr = dbcust.GetCustomerByFullName(fname);
            var getRental = dbhistory.GetRentalByCustomerFnameAndId(usr, id);
            var movieId = dbmovie.GetMovieIdOfRental(getRental);
            var movie = dbmovie.GetMovieById(movieId);
            var updateRental = new UpdateRentalBindingModel(getRental.Id, getRental.RenterFullName, getRental.RentalDate,
                getRental.ExpectedDate, getRental.ReturnDate, getRental.PhysicalCopyId, movie, getRental.UserId);

            /*if (updateRental == null)
            {
                return HttpNotFound();
            } */
            string value = string.Empty;
            value = JsonConvert.SerializeObject(updateRental, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            }); 

            var json = JsonConvert.SerializeObject(updateRental);
            return Json(json, JsonRequestBehavior.AllowGet);
        }
      
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult UpdateReturnDate(RentalsPerCustomerViewModel form)
        {
            CreateRentalBindingModel rentalform = new CreateRentalBindingModel(form.history.Id, form.history.RenterFullName, form.history.RentalDate,
                form.history.ExpectedDate, form.history.ReturnDate, form.history.PhysicalCopyId, form.history.MovieId, form.history.UserId);
            
            //History h = new History(rentalForm.Id, rentalForm.RenterFullName, rentalForm.RentalDate, rentalForm.ExpectedDate,
            //        rentalForm.ReturnDate, rentalForm.PhysicalCopyId, rentalForm.MovieId, rentalForm.UserId);
            var h = mapper.Map<History>(rentalform);
            dbhistory.UpdateRentalReturnDate(h);
            logger.Writer.Information("The selected rental just updated successfully!");

            string msg = "UPDATED SUCCESSFULLY!";
            return Json(new { Message = msg, JsonRequestBehavior.AllowGet });
        }
    }
}