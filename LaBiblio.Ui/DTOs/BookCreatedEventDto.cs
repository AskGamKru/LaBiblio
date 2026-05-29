namespace LaBiblio.Ui.DTOs
{
    public record BookCreatedEventDto
    {
        public Guid BookId { get; set; }
        public string Title { get; set; } = "";
        public string Author { get; set; } = "";
    }
}
