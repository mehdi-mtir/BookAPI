namespace BookAPI.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateOnly PublishDate { get; set; }
        public Decimal Price { get; set; } = new Decimal(0);
        public Author Author { get; set; }
        public int AuthorId {  get; set; } 
    }
}
