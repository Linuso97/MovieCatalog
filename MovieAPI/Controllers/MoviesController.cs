using Microsoft.AspNetCore.Mvc;
using MovieAPI.Models;
using MovieAPI.Services;

namespace MovieAPI.Controllers
{
    /*
    Summary: This controller handles HTTP requests related to movie operations. 
    It uses the MovieService to interact with the OMDb API and the local database, providing the following endpoints:

    1. GET /api/movies/{title}: Fetches movie details from the OMDb API based on the title.
    2. GET /api/movies: Retrieves all movies saved in the local database.
    3. POST /api/movies/save?title={title}: Saves a movie to the database if it exists in the OMDb API.
    4. DELETE /api/movies/{id}: Deletes a movie from the local database by its ID.

    The controller returns appropriate HTTP responses, such as 404 for not found or 204 for successful deletion.
    */
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController : Controller
    {
        private readonly MovieService _movieService;

        public MoviesController(MovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet("{title}")]
        public async Task<ActionResult<Movie>> GetMovie(string title)
        {
            var movie = await _movieService.GetMovieAsync(title);

            if (movie is null)
            {
                return NotFound();
            }

            return Ok(movie);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMovies()
        {
            var movies = await _movieService.GetAllMoviesAsync();
            return Ok(movies);
        }

        [HttpPost("save")]
        public async Task<IActionResult> SaveMovie(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                return BadRequest("Movie title is missing.");
            }

            var movie = await _movieService.GetMovieAsync(title);
            if (movie == null)
            {
                return NotFound("Movie not found.");
            }

            var savedMovie = await _movieService.SaveMovieAsync(movie);
            return Ok(savedMovie);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var result = await _movieService.DeleteMovieAsync(id);
            if(!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
