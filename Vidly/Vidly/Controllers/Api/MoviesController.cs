﻿using AutoMapper;
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
    public class MoviesController : ApiController
    {
        //DB
        private ApplicationDbContext _dbContext;

        public MoviesController()
        {
            _dbContext = new ApplicationDbContext();
        }
        

        //Methods
        public IEnumerable<MovieDto> GetMovies()
        {
            return _dbContext.Movies.ToList().Select(Mapper.Map<Movie,MovieDto>);
        }

        public IHttpActionResult GetMovie(int id)
        {
            var movie = _dbContext.Movies.SingleOrDefault(m => m.Id == id);

            if (movie == null)
                return NotFound();

            return Ok(Mapper.Map<Movie,MovieDto>(movie));
        }

        [HttpPost]  // /api/customers
        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var movie = Mapper.Map<MovieDto, Movie>(movieDto);

            _dbContext.Movies.Add(movie);
            _dbContext.SaveChanges();

            //because of autogenerated id
            movieDto.Id = movie.Id;


            return Created(new Uri(Request.RequestUri+"/"+movie.Id),movieDto);
        }

        [HttpPut]
        public void UpdateMovie(int id, MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var movieInDb = _dbContext.Movies.SingleOrDefault(m => m.Id == id);

            if (movieInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            // instead of setting each var
            Mapper.Map(movieDto, movieInDb);

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
