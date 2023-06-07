using Library.Domain.Common;

namespace Library.Domain.Entities
{
    public class Users : Entity
    {
        public long? UserId { get; set; }
        public string? NameAr { get; set; }
        public string? NameEn { get; set; }
        public long RoleId { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public List<UsersBooks> UsersBooks { get; set; }
    }
}
