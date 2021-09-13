using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VideoClubProject1.Core.Interfaces;
using VideoClubProject1.Infrastructure.Services;
using VideoClubProject1.Web.Areas.Copies.Models;

namespace VideoClubProject1.Web.Areas.Copies.Controllers
{
    public class CopiesController : Controller
    {

        private readonly ICopyService dbcopy;
        private readonly ILoggingService logger;

        public CopiesController(ICopyService db_copy, ILoggingService log)
        {
            dbcopy = db_copy;
            logger = log;
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult Details(int id)
        {
            string query = dbcopy.GetAvailableCopyOfMovie(id);
            int count = dbcopy.GetNumberOfAvailableCopies(id);
            var model = dbcopy.GetFirstAvailableCopy(query);
            if (model == null || count == 0)
            {
                return View("NotFound");
            }
            model.NumOfCopies = count;
            var availability = new CopyAvailabilityViewModel(model.Availability, model.NumOfCopies);
            logger.Writer.Information("Get availability of selected movie");
            return View(availability);
        }
    }
}