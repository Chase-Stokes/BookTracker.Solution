using System.Collections.Generic;

namespace Library.Models
{
  public class Copy
  {
    // public Copy()
    // {
    //   this.JoinEntities = new HashSet<Checkout>();
    // }

    public int CopyId { get; set; }
    public int BookId { get; }

    // public int TotalCopies { get; set; }
    public virtual ApplicationUser User { get; set; }
    public virtual Book Book { get; set; }

    // public virtual ICollection<Checkout> JoinEntities { get; set; }
    //new test
    public int PatronId { get; set; }
    public virtual Patron Patron { get; set; }
    public bool Checkout { get; set; } = false;
  }
}