using MovieCatalog.UI;

namespace MovieCatalog
{
    // Entry point for the application, which starts by showing the menu to the user.
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Menu menu = new Menu();

            await menu.ShowMenu();
        }
    }
}
