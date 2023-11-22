namespace joy_database.Models;

public class Story : DbEntity
{
   public string Name { get; set; } 
   public string Text { get; set; }
   public List<Media> Media { get; set; } = new List<Media>();
   public bool Approved { get; set; }
}