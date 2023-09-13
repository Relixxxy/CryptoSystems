using CryptoSystems.ViewModels;

namespace CryptoSystems.Services.Interfaces;

public interface ILaboratoryWorkService
{
    Task<IEnumerable<LaboratoryWorkListViewModel>> GetListAsync();
    Task<LaboratoryWorkViewModel> GetByIdAsync(int id);
}
