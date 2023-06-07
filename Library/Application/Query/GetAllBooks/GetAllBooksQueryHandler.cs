using Library.Domain.Entities;
using Library.Domain.Enums;
using Library.Domain.Extension;
using Library.Domain.Helpers;
using Library.Infrastructure.Common.GenericRepo;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Library.Application.Query.GetAllBooks
{
    public class GetAllBooksQueryHandler : IRequestHandler<GetAllBooksQuery, ResponseDTO>
    {
        private readonly IGRepository<Books> _booksRepository;
        private readonly ResponseDTO _responseDTO;
        private readonly ILogger<GetAllBooksQueryHandler> _logger;

        public GetAllBooksQueryHandler(
            IGRepository<Books> booksRepository,
            IHttpContextAccessor _httpContextAccessor,
            ILogger<GetAllBooksQueryHandler> logger
            )
        {
            _responseDTO = new ResponseDTO();
            _logger = logger;
            _booksRepository = booksRepository;
        }

        public async Task<ResponseDTO> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
        {
            try
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
            catch (Exception ex)
            {
                _responseDTO.Result = null;
                _responseDTO.Message = "anErrorOccurredPleaseContactSystemAdministrator";
                Console.WriteLine(ex.Message + (ex != null && ex.InnerException != null ? ex.InnerException.Message : ""));
                _logger.LogError(ex, ex.Message, (ex != null && ex.InnerException != null ? ex.InnerException.Message : ""));
            }
            return _responseDTO;
        }
    }


}