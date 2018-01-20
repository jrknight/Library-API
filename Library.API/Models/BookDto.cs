namespace Library.API.Models
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public int AuthorId { get; set; }
        public string Genre { get; set; }
        public string ISBN { get; set; }
    }
}
