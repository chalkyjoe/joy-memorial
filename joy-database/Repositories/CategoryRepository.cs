using joy_database.Data;
using joy_database.Models;
using Microsoft.EntityFrameworkCore;

namespace joy_database.Repositories;

public class CategoryRepository
{
    private readonly ApplicationDbContext _dbContext;

    public CategoryRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
     
    public async Task<int> Create(Category category)
    {
        await _dbContext.Category.AddAsync(category);
        return await _dbContext.SaveChangesAsync();
    }

    public Task<List<Category>> GetAll()
    {
        return _dbContext.Category.ToListAsync();
    }

    public Task<List<Category>> GetAllIncludingStories()
    {
        return _dbContext.Category.Include(m => m.Stories).ThenInclude(m => m.Media).ToListAsync();
    }

    public Task<Category> Get(Guid categoryId)
    {
        return _dbContext.Category.FirstOrDefaultAsync(m => m.Id == categoryId);
    }

    public async Task Update(Category category)
    {
        _dbContext.Category.Update(category);
        await _dbContext.SaveChangesAsync();
    }
}