using System.Collections.Generic;

namespace Library.Models
{
  public class Patron
  {
    public Patron()
    {
      // this.JoinEntities = new HashSet<Checkout>();
      this.JoinEntitiesOne = new HashSet<Copy>();
    }

    public int PatronId { get; set; }
    public string Name { get; set; }
    public virtual ApplicationUser User { get; set; }
    // public virtual ICollection<Checkout> JoinEntities { get; }
    public virtual ICollection<Copy> JoinEntitiesOne { get; }

  }
}