namespace joy_database.Models;

public class Story : DbEntity
{
   public string Name { get; set; } 
   public string Text { get; set; }
   public virtual List<Media> Media { get; set; } = new List<Media>();
   public virtual Category Category { get; set; }
   public Guid CategoryId { get; set; }
   public bool Approved { get; set; }
}