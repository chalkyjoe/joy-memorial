using System.ComponentModel.DataAnnotations.Schema;

namespace joy_database.Models;

public class Category : DbEntity
{
    public string Name { get; set; }
    public string Text { get; set; }
    public string ImageUrl { get; set; }
    public List<Story> Stories { get; set; } = new List<Story>();
    [NotMapped]
    public IEnumerable<Story> ApprovedStories => Stories.Where(m => m.Approved);
}