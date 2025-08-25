namespace ContactApi.Models
{
    public class ContactForm
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Phone { get; set; }
        public required string Email { get; set; }
        public required string Message { get; set; }
    }
}

