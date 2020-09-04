using IPAnalyzer.Services.Models.Robots;
using System.Threading.Tasks;

namespace IPAnalyzer.Services.Interfaces
{
    public interface IRobotsService
    {
        Task<RobotsInfo> GetRobotsInfoAsync(string url);
    }
}