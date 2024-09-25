using MovieCatalog.Services;

namespace MovieCatalog.UI
{
    /*
     Summary: This class is responsible for displaying the menu to the user and handling their selections. 
     The menu provides four main options:
     1. Search and add a movie to the catalog.
     2. Display the user's current movie list.
     3. Delete a movie from the list by providing its ID.
     4. Exit the program.
     The class uses a loop to continuously display the menu until the user chooses to exit. 
     It relies on the MovieApiClient to interact with the movie API for searching, saving, retrieving, and deleting movies.
    */
    internal class Menu
    {
        private MovieApiClient movieApiClient = new MovieApiClient();
        public async Task ShowMenu()
        {
            while (true)
            {
                Console.WriteLine($"Movie Catalog\n" +
                  "Pick between 1-4\n" +
                  "----------------\n" +
                  "1. Search and add movie\n" +
                  "2. Your movielist\n" +
                  "3. Delete movie from list\n" +
                  "4. Exit program.");

                var input = Console.ReadLine();
                if (int.TryParse(input, out int validInput))
                {
                    switch (validInput)
                    {
                        case 1:
                            Console.Write("Enter movie title: ");
                            string title = Console.ReadLine();

                            await movieApiClient.SearchAndSaveMovie(title);
                            Console.WriteLine("Press any button to continue..");
                            Console.ReadKey();
                            Console.Clear();
                            break;

                        case 2:
                            await movieApiClient.GetMovieList();
                            Console.WriteLine("Press any button to continue..");
                            Console.ReadKey();
                            Console.Clear();
                            break;

                        case 3:
                            Console.Write("Enter ID of the movie you'd like to remove: ");
                            string case3Input = Console.ReadLine();

                            if (int.TryParse(case3Input, out int movieid))
                            {
                                await movieApiClient.DeleteMovie(movieid);
                            }
                            else
                            {
                                Console.WriteLine("Invalid input. Please enter a valid numeric ID.");
                            }

                            Console.WriteLine("Press any button to continue..");
                            Console.ReadKey();
                            Console.Clear();
                            break;

                        case 4:
                            Environment.Exit(0);
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input, try again.");
                    Console.WriteLine("Press any button to continue..");
                    Console.ReadKey();
                    Console.Clear();
                }
            }

            

        }
    }
}
