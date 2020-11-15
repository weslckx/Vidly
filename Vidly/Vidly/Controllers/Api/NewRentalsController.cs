using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class NewRentalsController : ApiController
    {
        private ApplicationDbContext _dbcontext;

        public NewRentalsController(ApplicationDbContext dbContext)
        {
            _dbcontext = dbContext;
        }


        [HttpPost]
        public IHttpActionResult CreateNewRentals(NewRentalDto newRental)
        {
            if (newRental.MovieIds.Count == 0)
                return BadRequest("No MovieIds have been given");

            var customer = _dbcontext.Customers.SingleOrDefault(
                c => c.Id == newRental.CustomerId);

            if (customer == null)
                return BadRequest("CustomerId is not Valid");


            

            var movies = _dbcontext.Movies.Where(
                m => newRental.MovieIds.Contains(m.Id)).ToList();

            if (movies.Count != newRental.MovieIds.Count)
                return BadRequest("One or more movieIds are invalid");

            

            foreach (var movie in movies)
            {
                if (movie.NumberAvailable == 0)
                    return BadRequest("Movie is not available!");

                movie.NumberAvailable--;

                var rental = new Rental
                {
                    Customer = customer,
                    Movie = movie,
                    DateRented = DateTime.Now
                };

                _dbcontext.Rentals.Add(rental);
            }

            _dbcontext.SaveChanges();

            return Ok();
        }
    }
}
