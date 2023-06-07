using AutoMapper;
using Library.Application.Query.GetAllBooks;
using Library.Domain.Entities;
using Library.Domain.Helpers;
using Library.Infrastructure.Common.GenericRepo;
using MediatR;

namespace Library.Application.Command.PostBooks
{
    public class PostBooksCommandHandler : IRequestHandler<PostBooksCommand, ResponseDTO>
    {
        private readonly IGRepository<Books> _booksRepository;
        private readonly ResponseDTO _responseDTO;
        private readonly ILogger<GetAllBooksQueryHandler> _logger;
        private readonly IMapper _mapper;
        public PostBooksCommandHandler(
            IGRepository<Books> booksRepository,
            IHttpContextAccessor _httpContextAccessor,
            IMapper mapper,
            ILogger<GetAllBooksQueryHandler> logger
            )
        {
            _responseDTO = new ResponseDTO();
            _logger = logger;
            _booksRepository = booksRepository;
            _mapper = mapper;
        }

        public async Task<ResponseDTO> Handle(PostBooksCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entityObj = _mapper.Map<Books>(request);

                var timeNow = DateTime.Now;
                entityObj.CreatedOn = timeNow;
                entityObj.UpdatedOn = timeNow;

                entityObj.CreatedBy = null;
                entityObj.UpdatedBy = null;

                await _booksRepository.AddAsync(entityObj);
                _booksRepository.Save();

                _responseDTO.Result = null;
                _responseDTO.Message = "booksAddedSuccessfully";

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
