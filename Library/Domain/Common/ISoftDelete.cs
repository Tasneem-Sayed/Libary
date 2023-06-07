
using Library.Domain.Enums;

namespace Library.Domain.Common
{
  public interface ISoftDelete
  {
        State State { get; set; }
  }
}
