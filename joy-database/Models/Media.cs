namespace joy_database.Models;

public class Media : DbEntity
{
    public Media()
    {
        Id = Guid.NewGuid();
        Created = DateTime.Now;
        Approved = false;
    }
    public Story Story { get; set; }
    public string Filename { get; set; }
    public bool Approved { get; set; }
    public MediaType Type { get; set; }
}

public enum MediaType
{
    Image,
    Video,
    Audio,
    Youtube
}