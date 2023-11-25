using joy_database.Data;
using joy_database.Models;
using Microsoft.EntityFrameworkCore;

namespace joy_database.Repositories;

public class MediaRepository
{
    private readonly ApplicationDbContext _dbContext;

    public MediaRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<List<Media>> GetAllApprovedMedia()
    {
        return _dbContext.Media.Include(m => m.Story).Where(m => m.Story.Approved).ToListAsync();
    }
}