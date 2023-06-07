using Library.Domain.Entities;
using Library.Domain.Helpers;
using MediatR;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Application.Command.PostBooks
{
    public class PostBooksCommand : IRequest<ResponseDTO>
    {
        public string? NameAr { get; set; }
        public string? NameEn { get; set; }
        public decimal? Price { get; set; }
        public long CategoryId { get; set; }
        public long AuthorId { get; set; }

    }
}
