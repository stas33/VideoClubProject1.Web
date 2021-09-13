using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VideoClubProject1.Core.Entities;
using VideoClubProject1.Core.Enumerations;
using VideoClubProject1.Core.Interfaces;
using VideoClubProject1.Infrastructure.Services;
using VideoClubProject1.Web.Areas.Users.Models;

namespace VideoClubProject1.Web.Areas.Users.Controllers
{
    public class UsersController : Controller
    {

        private readonly ICustomerService dbcust;
        private readonly IHistoryService dbhistory;
        private readonly ILoggingService logger;


        public UsersController(ICustomerService db_cust, IHistoryService db_history, ILoggingService log)
        {
            dbcust = db_cust;
            dbhistory = db_history;
            logger = log;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult Index()
        {

            var customers = dbcust.GetCustomers();
            var customerslist = new CustomerViewModel(customers);
            logger.Writer.Information("Display the customers list");
            return View(customerslist);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult ActiveRentals(string id)
        {
            var histories = dbhistory.GetRentalsPerCustomer(id);
            if (histories == null)
            {
                return View("NotFound");
            }
            var RentalsOfCustomer = new RentalsPerCustomerViewModel(histories);
            logger.Writer.Information("Display the active rentals of the selected customer");
            return View(RentalsOfCustomer);
        }

    }
}