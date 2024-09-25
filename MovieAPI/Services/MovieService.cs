using Microsoft.EntityFrameworkCore;
using MovieAPI.Data;
using MovieAPI.Models;
using System.Text.Json;

namespace MovieAPI.Services
{
    /*
    Summary: This service class provides methods to interact with both the OMDb API and the local database 
    for managing movie data. The class includes the following functionality:
 
    1. GetMovieAsync: Fetches movie details from the OMDb API based on the movie title.
    2. GetAllMoviesAsync: Retrieves all movies stored in the local database.
    3. SaveMovieAsync: Saves a movie to the database if it doesn't already exist based on title and year.
    4. DeleteMovieAsync: Deletes a movie from the database by its ID.

    The service makes use of HttpClient for external API requests and Entity Framework Core for database operations.
    */
    public class MovieService
    {
        private readonly HttpClient _httpClient;
        private readonly DataContext _context;

        public MovieService(HttpClient httpClient, DataContext context)
        {
            _httpClient = httpClient;
            _context = context;
        }

        public async Task<Movie?> GetMovieAsync(string title)
        {
            var response = await _httpClient.GetStringAsync($"http://www.omdbapi.com/?t={title}&apikey=c74d46b0");
            var movie = JsonSerializer.Deserialize<Movie>(response);

            return movie;
        }

        public async Task<List<Movie>> GetAllMoviesAsync()
        {
            return await _context.Movies.ToListAsync();
        }

        public async Task<Movie> SaveMovieAsync(Movie movie)
        {
            var existingMovie = await _context.Movies
                .FirstOrDefaultAsync(m => m.Title == movie.Title && m.Year == movie.Year);

            if (existingMovie is null)
            {
                _context.Movies.Add(movie);
                await _context.SaveChangesAsync();
            }

            return movie;
        }

        public async Task<bool> DeleteMovieAsync(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie is null)
            {
                return false;
            }

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();

            return true;
        }

    }
}
