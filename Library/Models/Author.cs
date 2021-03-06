using System.Collections.Generic;

namespace Library.Models
{
  public class Author
  {
    public Author()
    {
      this.JoinEntities = new HashSet<AuthorBook>();
    }
    public int AuthorId { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public string Description { get; set; }
    public string Search { get; set; }
    public virtual ICollection<AuthorBook> JoinEntities { get; set; }
  }
}