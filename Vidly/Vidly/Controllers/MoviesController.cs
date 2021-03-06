﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {

        private ApplicationDbContext _dbContext;

        public MoviesController()
        {
            _dbContext = new ApplicationDbContext(); 
        }

        protected override void Dispose(bool disposing)
        {
            _dbContext.Dispose();
        }

        // GET: Movies
        public ActionResult Index()
        {

            return View(_dbContext.Movies.Include(m=>m.Genre).ToList());
        }

        //public ActionResult Details(int id)
        //{
        //    var movie = _dbContext.Movies.Include(m=>m.Genre).SingleOrDefault(c => c.Id == id);

        //    if (movie == null)
        //        return HttpNotFound();

        //    return View(movie);
        //}

        public ActionResult New()
        {
            var viewModel = new MovieFormViewModel
            {
                Genres = _dbContext.Genres.ToList(),
              //  Movie = new Movie() // otherwise in validationsummery, error for our hidden field ID, now id=0
              // removed, now fixed in Viewmodel
            };

            ViewData["Task"] = "New Movie";
            return View("MovieForm",viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Movie movie)
        {
            if(!ModelState.IsValid)
            {
                var viewModel = new MovieFormViewModel(movie)
                {
                    Genres = _dbContext.Genres.ToList()
                };
                return View("MovieForm", viewModel);
            }
                

            if (movie.Id==0)
            {
                movie.AddDate = DateTime.Now;
                _dbContext.Movies.Add(movie);
            }
            else
            {
                var movieInDb = _dbContext.Movies.Single(m => m.Id == movie.Id);

                movieInDb.Title = movie.Title;
                movieInDb.GenreId = movie.GenreId;
                movieInDb.Stock = movie.Stock;
                movieInDb.ReleaseDate = movie.ReleaseDate;
                
            }
            
            _dbContext.SaveChanges();

            return RedirectToAction("Index", "Movies");
        }

        public ActionResult Edit(int id)
        {
            var movie = _dbContext.Movies.SingleOrDefault(m => m.Id == id);

            if (movie == null)
                return HttpNotFound();

            var viewModel = new MovieFormViewModel(movie)
            {
                Genres = _dbContext.Genres.ToList(),
                //Movie = movie
                // fix in viewmodel, now individuel properties. But cleaner to put it in viewmodel (constructor)
            };

            ViewData["Task"] = "Edit Movie";

            return View("MovieForm", viewModel);




        }
    }
}