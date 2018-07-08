namespace SimpleService.Model
{
    public class BookRating
    {
        public int Id { get; set; }
        public string BookId { get; set; }
        public string BookTitle { get; set; }
        public float Stars { get; set; }
    }
}
