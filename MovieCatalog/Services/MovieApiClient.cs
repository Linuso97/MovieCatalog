using System.Net.Http.Json;
using MovieAPI.Models;

namespace MovieCatalog.Services
{
    /* 
    Summary: This service encapsulates all the methods necessary for managing 
    movie-related operations within the application. 
    It interacts with the API to access various HTTP methods, enabling 
    data retrieval and storage in the SQL Server database. 
    Each method includes error handling using try-catch blocks to gracefully
    manage potential network issues, as the application relies on saving
    information to a database rather than a local list.
    */
    internal class MovieApiClient
    {
        // Static HttpClient instance to avoid socket exhaustion.
        private static readonly HttpClient httpClient = new HttpClient();

        public MovieApiClient()
        {
            // Set base-URL for API.
            httpClient.BaseAddress = new Uri("https://localhost:7231"); // Change to your port number.
        }

        public async Task SearchAndSaveMovie(string title)
        {
            try
            {
                var response = await httpClient.GetAsync($"api/movies/{title}");

                if (response.IsSuccessStatusCode)
                {
                    var movie = await response.Content.ReadFromJsonAsync<Movie>();

                    if (movie == null)
                    {
                        Console.WriteLine("No movie data found.");
                        return; 
                    }

                    Console.WriteLine($"Title: {movie.Title}\n" +
                                      $"Year: {movie.Year}\n" +
                                      $"Genre: {movie.Genre}\n" +
                                      $"Plot: {movie.Plot}\n" +
                                      $"Director: {movie.Director}\n" +
                                      $"Actors: {movie.Actors}");

                    Console.WriteLine("Write 'y' to save movie to your list or anything else to not.");
                    var input = Console.ReadLine();

                    if (input?.ToLower() == "y")
                    {
                        var saveMovieResponse = await httpClient.PostAsync($"api/movies/save?title={movie.Title}", null);


                        if (saveMovieResponse.IsSuccessStatusCode)
                        {
                            Console.WriteLine("Movie saved.");
                        }
                    } else
                    {
                        Console.WriteLine("Not saved.");
                    }
                }
                else
                {
                    Console.WriteLine("Movie could not be found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public async Task DeleteMovie(int movieid)
        {
            try
            {
                var response = await httpClient.DeleteAsync($"api/movies/{movieid}");

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Movie with ID {movieid} deleted successfully.");
                }
                else
                {
                    Console.WriteLine($"No movie with ID {movieid} was found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }


        }

        public async Task GetMovieList()
        {
            try
            {
                var response = await httpClient.GetAsync("api/movies");
                int i = 0;

                if (response.IsSuccessStatusCode)
                {
                    var movies = await response.Content.ReadFromJsonAsync<List<Movie>>();

                    if (movies?.Count > 0)
                    {
                        foreach (var movie in movies)
                        {
                            i++;
                            Console.WriteLine($"Nr.{i} Title: {movie.Title} | Year: {movie.Year} | Genre: {movie.Genre} | ID: {movie.Id}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No movies were found");
                    }
                }
                else
                {
                    Console.WriteLine("No movies were found");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

        }
    }
}
