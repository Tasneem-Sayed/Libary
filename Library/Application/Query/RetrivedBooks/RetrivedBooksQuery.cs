using Library.Domain.Helpers;
using MediatR;

namespace Library.Application.Query.RetrivedBooks
{
    public class RetrivedBooksQuery : IRequest<ResponseDTO>
    {
        public DateTime Date { get; set; }
        public long? BookId { get; set; }
        public long? UserId { get; set; }
    }
}
