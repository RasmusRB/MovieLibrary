using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ModelLib.Model;
using MovieREST.Managers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MovieREST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {

        MovieManager mng = new MovieManager();

        // GET: api/<MoviesController>
        [HttpGet]
        public IEnumerable<Movie> Get()
        {
            return mng.GetAll();
        }

        // GET api/<MoviesController>/5
        [HttpGet]
        [Route("{id}")]
        public Movie Get(int id)
        {
            return mng.GetMovieFromId(id);
        }

        // GET api/<MoviesController>/5
        [HttpGet]
        [Route("SortByRating/{rating}")]
        public List<Movie> GetByRating(double rating)
        {
            return mng.GetMovieByRating(rating);
        }

        // POST api/<MoviesController>
        [HttpPost]
        public bool Post([FromBody] Movie value)
        {
            return mng.CreateMovie(value);
        }

        // PUT api/<MoviesController>/5
        [HttpPut("{id}")]
        public bool Put(int id, [FromBody] Movie value)
        {
            return mng.UpdateMovie(value, id);
        }

        // DELETE api/<MoviesController>/5
        [HttpDelete("{id}")]
        public Movie Delete(int id)
        {
            return mng.DeleteMovie(id);
        }
    }
}
