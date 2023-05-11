using IKApplication.Application.VMs.SectorVMs;

namespace IKApplication.Application.AbstractServices
{
    public interface ISectorService
    {
        Task<List<SectorVM>> GetAllSectors();
    }
}
