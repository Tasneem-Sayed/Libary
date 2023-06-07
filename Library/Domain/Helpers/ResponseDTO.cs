namespace Library.Domain.Helpers
{
    public class ResponseDTO
    {
        public dynamic Result { get; set; }

        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
        public int TotalItems { get; set; }
        public int PageSize { get; set; }

        public string Message { get; set; }

       
    }
}

