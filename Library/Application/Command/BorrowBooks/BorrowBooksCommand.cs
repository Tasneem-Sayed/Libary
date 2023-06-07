using Library.Domain.Helpers;
using MediatR;

namespace Library.Application.Command.BorrowBooks
{
    public class BorrowBooksCommand : IRequest<ResponseDTO>
    {
        public List<ModelFiltering>? Filter { get; set; }

        public int Period { get; set; }
        public long? BookId { get; set; }
        public long? UserId { get; set; }



    }
}
