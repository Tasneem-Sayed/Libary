using Library.Application.Command.BorrowBooks;
using Library.Application.Command.PostBooks;
using Library.Application.Query.GetAllBooks;
using Library.Application.Query.RetrivedBooks;
using Library.Domain.Helpers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IMediator _mediator;
        public BooksController(IMediator mediator)
        {
            _mediator = mediator;
        }

    
      //GetAllBooksWithSearch
        [HttpPost]
        [Route("GetAllBooKs")]
        public async Task<ResponseDTO> GetAllBooks( List<ModelFiltering>? Filter)
        {
            return await _mediator.Send(new GetAllBooksQuery()
            {
                Filter = Filter
            });
        }

        [HttpGet]
        [Route("GetFine")]
        public async Task<ResponseDTO> GetFineBooks(RetrivedBooksQuery query)
        {
            return await _mediator.Send(query);
        }

        //PostBorrowBooks
        [HttpPost]
        [Route("BorrowBooks")]
        public async Task<ResponseDTO> BorrowBooks(BorrowBooksCommand BorrowBooks)
        {
            return await _mediator.Send(BorrowBooks);
        }

        //CreateBooks

        [HttpPost]
        public async Task<ResponseDTO> PostBooks(PostBooksCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}
