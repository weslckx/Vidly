using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class MoviesController : ApiController
    {
        //DB
        private ApplicationDbContext _dbContext;

        public MoviesController()
        {
            _dbContext = new ApplicationDbContext();
        }
        

        //Methods
        public IEnumerable<Movie> GetMovies()
        {
            return _dbContext.Movies.ToList();
        }

        public Movie GetMovie(int id)
        {
            var movie = _dbContext.Movies.SingleOrDefault(m => m.Id == id);

            if (movie == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return movie;
        }

        [HttpPost]  // /api/customers
        public Movie CreateMovie(Movie movie)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            _dbContext.Movies.Add(movie);
            _dbContext.SaveChanges();

            return movie;
        }

        [HttpPut]
        public void UpdateMovie(int id, Movie movie)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var movieInDb = _dbContext.Movies.SingleOrDefault(m => m.Id == id);

            if (movieInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            movieInDb.Title = movie.Title;
            movieInDb.GenreId = movie.GenreId;
            movieInDb.ReleaseDate = movie.ReleaseDate;
            movieInDb.Stock = movie.Stock;

            _dbContext.SaveChanges();
        }

        [HttpDelete]
        public void DeleteMovie(int id)
        {
            var movie = _dbContext.Movies.SingleOrDefault(m => m.Id == id);

            if (movie == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _dbContext.Movies.Remove(movie);
            _dbContext.SaveChanges();


        }
    }
}
