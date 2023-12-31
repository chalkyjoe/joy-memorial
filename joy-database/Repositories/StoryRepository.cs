using joy_database.Data;
using joy_database.Models;
using Microsoft.EntityFrameworkCore;

namespace joy_database.Repositories;

public class StoryRepository
{
    private readonly ApplicationDbContext _dbContext;

    public StoryRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<int> Create(Story story)
    {
        story.Id = new Guid();
        story.Created = DateTime.Now;
        _dbContext.Stories.Add(story);
        return _dbContext.SaveChangesAsync();
    }

    public Task<List<Story>> GetAllApproved(Guid categoryId)
    {
        var stories = _dbContext.Stories.Include(m => m.Media).Include(m => m.Category).Where(m => m.Approved && m.CategoryId == categoryId).ToListAsync();
        return stories;
    }

    public Task<List<Story>> GetAllUnApproved()
    {
        var stories = _dbContext.Stories.Include(m => m.Media).Include(m => m.Category).Where(m => !m.Approved).ToListAsync();
        return stories;
    }

    public Task<int> Approve(Story story)
    {
        story.Approved = true;
        _dbContext.Stories.Update(story);
        return _dbContext.SaveChangesAsync();
    }

    public async Task Delete(Guid storyId)
    {
        _dbContext.Stories.Where(m => storyId == m.Id).ExecuteDeleteAsync();
    }
}