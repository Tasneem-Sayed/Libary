using Library.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Library.Domain.Entities;

namespace Library.Domain.Common
{
    public class Entity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
        public long Id { get; set; }

        public DateTime CreatedOn { set; get; }
        public long? UpdatedBy { set; get; }
        public DateTime? UpdatedOn { set; get; }
        public State State { get; set; }

        [ForeignKey("User")]
        public long? CreatedBy { set; get; }
        public Users User { get; set; }
    }
}
