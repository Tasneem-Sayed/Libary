using AutoMapper;
using Library.Application.Query.GetAllBooks;
using Library.Domain.Entities;
using Library.Domain.Enums;
using Library.Domain.Helpers;
using Library.Infrastructure.Common.GenericRepo;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Library.Application.Query.RetrivedBooks
{
    public class RetrivedBooksQueryHandler : IRequestHandler<RetrivedBooksQuery, ResponseDTO>
    
    { private readonly IGRepository<Books> _booksRepository;
        private readonly IGRepository<UsersBooks> _usersBooksRepository;
        private readonly ResponseDTO _responseDTO;
        private readonly IMapper _mapper;
      

        public RetrivedBooksQueryHandler(
            IGRepository<Books> booksRepository,
            IGRepository<UsersBooks> usersBooks,
            IHttpContextAccessor _httpContextAccessor,
            IMapper mapper
            )
        {
            _responseDTO = new ResponseDTO();
            _booksRepository = booksRepository;
            _usersBooksRepository = usersBooks;
            _mapper = mapper;
        }

        public async Task<ResponseDTO> Handle(RetrivedBooksQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var fine = new double();
                var books = _booksRepository.GetAll(x => x.State != State.Deleted && x.Id == request.BookId && x.IsBorrowed == false)
                          .Include(x => x.Categories)
                          .Include(x => x.Author)
                          .Include(x => x.UsersBooks)
                          .FirstOrDefault();
                if (books != null)
                {
                    var duration = books.UsersBooks.Select(x => (x.BorrowedDate.Value.AddDays(x.Period))).FirstOrDefault();
                    if (request.Date != null &&( duration > DateTime.Now) )
                    {
                        var fineDate = (DateTime.Now - books.UsersBooks.Select(x => x.BorrowedDate).FirstOrDefault()).Value.Days.ToString();
                         fine=double.Parse( fineDate ) * 10;
                    }
                    books.IsBorrowed = false;
                    books.IsRetrieved = true;
                    await _booksRepository.AddAsync(books);
                    _booksRepository.Save();

                }
                _responseDTO.Result = fine != null ? fine  : null;
                _responseDTO.Message = "booksRetrivedSuccessfully";

            }
            catch (Exception)
            
            {
                _responseDTO.Result = null;
                _responseDTO.Message = "anErrorOccurredPleaseContactSystemAdministrator";
            }
            return _responseDTO;
        }
    }
}
