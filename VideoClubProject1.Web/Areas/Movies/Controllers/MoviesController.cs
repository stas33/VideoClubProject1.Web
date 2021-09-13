using AutoMapper;
using PagedList;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using VideoClubProject1.Core.Entities;
using VideoClubProject1.Core.Interfaces;
using VideoClubProject1.Infrastructure.Models;
using VideoClubProject1.Infrastructure.Services;
using VideoClubProject1.Web.Areas.Movies.Models;
using VideoClubProject1.Web.Mappings;
using static VideoClubProject1.Infrastructure.Services.MoviePagingService;

namespace VideoClubProject1.Web.Areas.Movies.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMovieService dbmovie;
        private readonly ICopyService dbcopy;
        private readonly IMoviePagingService dbpagingmovies;
        private readonly ILoggingService logger;

        private IMapper mapper => MapperInit.Init();

        public MoviesController(IMovieService db_movie, ICopyService db_copy, IMoviePagingService moviepaging, ILoggingService log)
        {
            dbmovie = db_movie;
            dbcopy = db_copy;
            dbpagingmovies = moviepaging;
            logger = log;
        }

        [HttpGet]
        public async Task<ActionResult> Index(int? movieType, string currentFilter, int? page = 1, int pageSize = 4, SortingOrder sortOrder = SortingOrder.TitleAscending)
        {
            int pageNumber = (page ?? 1);
            var pageDto = new PaginationDto(pageNumber, pageSize);
            ViewBag.TitleSortParm = sortOrder == SortingOrder.TitleAscending ? SortingOrder.TitleDescending : SortingOrder.TitleAscending;
            var pageModel = await dbpagingmovies.GetPaginatedMovies(movieType, currentFilter, sortOrder, pageDto);
            ViewBag.CurrentSort = sortOrder;
            ViewBag.CurrentFilter = currentFilter;
            ViewBag.CurType = movieType;

            var viewModel = new PaginationModel<MovieViewModel>
            {
                PageNum = pageModel.PageNum,
                PageSize = pageModel.PageSize,
                TotalItems = pageModel.TotalItems,
                Items = new List<MovieViewModel>(),
                PageCount = pageModel.PageCount
            };
            foreach (var item in pageModel.Items)
            {
                viewModel.Items.Add(new MovieViewModel
                {
                    Id = item.Id,
                    Title = item.Title,
                    Description = item.Description,
                    Type = item.Type
                });
            }
            logger.Writer.Information("Display the movies list...");
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var model = dbmovie.GetMovieById(id);
            if (model == null)
            {
                return View("NotFound");
            }
            var moviedetails = new MovieViewModel(model.Id, model.Title, model.Description, model.Type);
            logger.Writer.Information("Display the details of the movie with Id {Id}", id);
            return View(moviedetails);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Movie movie)
        {
            if (String.IsNullOrEmpty(movie.Title))
            {
                ModelState.AddModelError(nameof(movie.Title), "The Title is required");
            }
            if (!ModelState.IsValid)
            {
                return View();
            }
            dbmovie.AddMovie(movie);
            logger.Writer.Information("A new movie created!");
            dbcopy.AddCopy(movie);
            logger.Writer.Information("Three copies of the movie {@movie} created", movie);

            return RedirectToAction("Details", new { Area = "Movies", id = movie.Id });
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var getmovie = dbmovie.GetMovieById(id);
            var editForm = new MovieBindingModel(getmovie.Id, getmovie.Title, getmovie.Description, getmovie.Type);
            //var editForm = mapper.Map<Movie>(getmovie);
            if (editForm == null)
            {
                return HttpNotFound();
            }
            logger.Writer.Information("Editing the movie with Id {Id} ...", id);
            return View(editForm);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MovieBindingModel editForm)
        {
            if (!ModelState.IsValid)
            {
                return View(editForm);
            }
            //Movie edited = new Movie(editForm.Id, editForm.Title, editForm.Description, editForm.Type);
            var edited = mapper.Map<Movie>(editForm);
            dbmovie.EditMovie(edited);
            logger.Writer.Information("Movie edited successfully!");
            return RedirectToAction("Details", new { id = editForm.Id });
        }
    }
}