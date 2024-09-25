namespace MovieAPI.Models
{
    // Represents an object with basic properties 
    public class Movie
    {
        public int Id { get; set; } 
        public string Title { get; set; }
        public string Year { get; set; }
        public string Genre { get; set; }
        public string Plot { get; set; }
        public string Director { get; set; } 
        public string Actors { get; set; }  
    }
}
