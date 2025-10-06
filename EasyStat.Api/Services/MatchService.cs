using EasyStat.Api.Data;

namespace EasyStat.Api.Services;

public interface IMatchService
{
    // Add match-related methods here as needed
}

public class MatchService : IMatchService
{
    private readonly EasyStatDbContext _context;

    public MatchService(EasyStatDbContext context)
    {
        _context = context;
    }

    // Implement match-related methods here
}