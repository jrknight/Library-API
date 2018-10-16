namespace Library.API.Models
{
    public class ItemDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string OwnerEmail { get; set; }
        public string CurrentHolderEmail { get; set; }
        public string PhotoUrl { get; set; }
    }
}
