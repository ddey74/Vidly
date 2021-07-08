using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Models;
using AutoMapper;
using Vidly.DTOs;

namespace Vidly.Controllers.API
{


    public class MoviesController : ApiController
    {

        private ApplicationDbContext _context;
        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }






        //Get: /api/Movies
        public IHttpActionResult GetMovie()
        {
            var movieList = _context.Movies.ToList().Select(Mapper.Map<Movie, MovieDto>);
            return Ok(movieList); 
        }








        //Get: /api/Movies/1
        public IHttpActionResult GetMovie(int id)
        {
            var movie = _context.Movies.SingleOrDefault(c => c.Id == id);
            if(!ModelState.IsValid)
            {
                return NotFound();
            }
            return Ok(Mapper.Map<Movie, MovieDto>(movie));//movie;
        }







        //POST: /api/Movies
        [HttpPost]
        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            var movie = Mapper.Map<MovieDto, Movie>(movieDto);
            _context.Movies.Add(movie);
            _context.SaveChanges();

            movie.Id = movieDto.Id;
            return Created(new Uri(Request.RequestUri+"/"+movie.Id),movieDto);//movieDto;
        }







        //PUT: /api/Movies/1
        [HttpPut]
        public IHttpActionResult UpdateMovie(int id,MovieDto movieDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            var movieInDb = _context.Movies.SingleOrDefault(m => m.Id == id);
            if(movieInDb==null)
            {
                return NotFound();
            }
            Mapper.Map<MovieDto, Movie>(movieDto);
            //movieInDb.Name = movie.Name;
            //movieInDb.NumberInStock = movie.NumberInStock;
            //movieInDb.ReleaseDate = movie.ReleaseDate;
            //movieInDb.GenreId = movie.GenreId;

            _context.SaveChanges();
            return Ok();

        }






        //DELETE: /api/Movies/1
        [HttpDelete]
        public IHttpActionResult DeleteMovie(int id)
        {
            var movieInDb = _context.Movies.SingleOrDefault(m => m.Id == id);
            if (movieInDb == null)
            {
                return NotFound();
            }

            _context.Movies.Remove(movieInDb);
            _context.SaveChanges();

            return Ok();
        }
    }
}
