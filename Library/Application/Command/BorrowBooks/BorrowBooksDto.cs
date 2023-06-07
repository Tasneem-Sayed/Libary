namespace Library.Application.Command.BorrowBooks
{
    public class BorrowBooksDto
    {
        public long Period { get; set; }
        public long? BookId { get; set; }
        public long? UserId { get; set; }
    }
}
