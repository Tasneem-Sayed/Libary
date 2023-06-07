using Library.Domain.Common;

namespace Library.Domain.Entities
{
    public class Author : Entity
    {
        public string? NameAr { get; set; }
        public string? NameEn { get; set; }

        public List<Books> Books { get; set; }

    }
}
