using Library.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Domain.Entities
{
    public class UsersBooks : Entity
    {
        [ForeignKey("Books")]
        public long? BookId { get; set; }
        [ForeignKey("Users")]
        public long? UserId { get; set; }
        public List<Books> Books { get; set; }
        public List<Users> Users { get; set; }

        public double Period { get; set; }
        public DateTime? BorrowedDate { get; set; }
    }
}
