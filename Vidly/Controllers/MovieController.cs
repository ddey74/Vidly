using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModel;

namespace Vidly.Controllers
{
    public class MovieController : Controller
    {
        ApplicationDbContext _context;
        public MovieController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Movie/Random
        public ActionResult Random()
        {
            #region One Model only can be handled here
            //Thsi action method can handle only one model at a time
            //var movie = new Movie() { Name = "Die Hard" };
            //ViewData["Movie"] = movie;//viewdata see the view also
            //ViewBag.Movie = movie;//viewbag
            //return View(movie);//view() is method of ViewResult and we pass model in it
            #endregion

            #region ViewModel so we can handle multiple models
            var movie = new Movie() { Name = "Die Hard" };
            var customers = new List<Customer> {
                new Customer() {Name="Customer1" },
                new Customer() {Name="Customer2" },
                new Customer() {Name="Customer3" },
                new Customer() {Name="Customer4" },
                new Customer() {Name="Customer5" }
            };
            var viewModel = new RandomMovieViewModel()
            {
                Movie = movie,
                Customers = customers
            };
            return View(viewModel);//returning ViewModel to the view and we have to render 
            #endregion


        }
        public ActionResult Edit(int id)
        {
            return Content("id " + id);
        }
        // this will be called /movies,  as other parameters are optional
        //public ActionResult Index(int? PageIndex,string sortBy)
        //{
        //    //string can be null so no null chk required
        //    if (!PageIndex.HasValue)
        //        PageIndex = 1;
        //    if (String.IsNullOrWhiteSpace(sortBy))
        //        sortBy = "Name";
        //    return Content(String.Format("Page Index = {0} and Sort By = {1}", PageIndex, sortBy));
        //}

        //in the top part we can add attribute for attribute routing
        [Route("movie/released/{year}/{month:regex(\\d{2}):range(1,12)}")] //attribute routing
        public ActionResult ByReleasedDate(int year,int month)
        {
            return Content(year + "/" + month);//in conventional routing we can specify constraints, see RouteConfig.cs,commented out
        }

        public ActionResult Index()
        {
            var movies = GetMovies();//old code depending on static value for data
            //var movies=_context.
            return View(movies);
        }
        private IEnumerable<Movie> GetMovies()
        {
            return new List<Movie>
            {
                new Movie { Id = 1, Name = "Shrek" },
                new Movie { Id = 2, Name = "Wall-e" }
            };
        }
    }
}