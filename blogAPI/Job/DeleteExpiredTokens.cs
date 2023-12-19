
using blogAPI.Data;
using Quartz;

namespace blogAPI.Jobs;

public class DeleteExpiredTokensJob : IJob
{
    private readonly AppDbContext _context;

    public DeleteExpiredTokensJob(AppDbContext context)
    {
        _context = context;
    }
    public Task Execute(IJobExecutionContext context)
    {

        var invalidTokens = _context.Tokens;
        foreach (var invalidToken in invalidTokens.Where(invalidToken =>
                     invalidToken.ExpiredDate + TimeSpan.FromMinutes(JWTSettings.Lifetime) < DateTime.UtcNow))
        {
            invalidTokens.Remove(invalidToken);
        }
        _context.SaveChanges();

        return Task.FromResult(true);
    }
}