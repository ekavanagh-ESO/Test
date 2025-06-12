using MythTechTest.Models.Entities;

namespace MythTechTest.Repositories;


public interface ISportsEventRepository
{
    Task<SportsEvent> GetByIdAsync(string id);
}