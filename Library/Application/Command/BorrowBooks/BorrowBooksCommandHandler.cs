using AutoMapper;
using Library.Domain.Entities;
using Library.Domain.Enums;
using Library.Domain.Extension;
using Library.Domain.Helpers;
using Library.Infrastructure.Common.GenericRepo;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;

namespace Library.Application.Command.BorrowBooks
{
    public class BorrowBooksCommandHandler : IRequestHandler<BorrowBooksCommand, ResponseDTO>
    {
        private readonly IGRepository<Books> _booksRepository;
        private readonly IGRepository<UsersBooks> _usersBooksRepository;
        private readonly ResponseDTO _responseDTO;
        private readonly IMapper _mapper;
        public BorrowBooksCommandHandler(
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

        public async Task<ResponseDTO> Handle(BorrowBooksCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Filter != null && request.Filter.Count > 0)
                {
                    var result = _booksRepository.GetAll(x => x.State != State.Deleted)
                                       .Include(x => x.Categories)
                                       .Include(x => x.Author)
                                       .AsQueryable();
                    if (result != null && (request.Filter != null && request.Filter.Count > 0))
                    {
                        foreach (var item in request.Filter)
                        {
                            result = result.FilterDynamic(result, item.ColName, item.Value);
                        }

                    }

                    _responseDTO.Result = result.ToList();
                    _responseDTO.Message = "booksRetrievedSuccessfully";
                }  
                var books = _booksRepository.GetAll(x => x.State != State.Deleted && x.Id == request.BookId && x.IsBorrowed == false)
                            .Include(x => x.Categories)
                            .Include(x => x.Author)
                            .Include(x => x.UsersBooks)
                            .FirstOrDefault();
                if (books != null )
                {
                    books.IsBorrowed = true;
                    var userBooks = new UsersBooks()
                    {
                        Period = request.Period,
                        BookId = request.BookId,
                        UserId = request.UserId,
                        BorrowedDate=DateTime.Now
                    };
                _usersBooksRepository.Add(userBooks);
                 _usersBooksRepository.Save();

            }
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
