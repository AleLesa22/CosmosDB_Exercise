using FBAuthDemoAPI.Models;

namespace FBAuthDemoAPI.Services.Contract
{
    public interface IFamilyService
    {
        Task<IEnumerable<Family>> GetFamilyDataAsync(string query);
        Task AddFamilyDataAsync(Family family);
        Task DeleteFamilyDataAsync(string id);
        Task UpdateFamilyDataAsync(Family family);
    }
}
