using Microsoft.EntityFrameworkCore;
using MythTechTest.Data;
using MythTechTest.Models.Entities;
namespace MythTechTest.Repositories;


public class SportsEventRepository : ISportsEventRepository
{
    private readonly SportsEventContext _context;

    public SportsEventRepository(SportsEventContext context)
    {
        _context = context;
    }

    public async Task<SportsEvent> GetByIdAsync(string id)
    {
        return await _context.SportsEvents
            .Include(e => e.States)
            .Include(e => e.SportsOrganizations)
            .Include(e => e.ParentEvents)
            .Include(e => e.RelatedEvents)
            .Include(e => e.Meta)
            .FirstOrDefaultAsync(e => e.Id == id);
    }
} 
