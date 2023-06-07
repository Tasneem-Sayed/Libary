using Library.Domain.Helpers;
using MediatR;

namespace Library.Application.Query.GetAllBooks
{
    public class GetAllBooksQuery : IRequest<ResponseDTO>
    {
        public List<ModelFiltering>? Filter { get; set; }
    }
}
