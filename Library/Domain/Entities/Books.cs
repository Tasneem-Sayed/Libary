using Library.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Domain.Entities
{
    public class Books : Entity
    {
        public string? NameAr { get; set; }
        public string? NameEn { get; set; }
        public decimal? Price { get; set; }

        [ForeignKey("Categories")]
        public long CategoryId { get; set; }
        public Categories Categories { get; set; }

        [ForeignKey("Author")]
        public long AuthorId { get; set; }
        public Author Author { get; set; }
        public bool? IsRetrieved { get; set; }
        public bool? IsBorrowed { get; set; }

        public List<UsersBooks> UsersBooks { get; set; }
    }
}
