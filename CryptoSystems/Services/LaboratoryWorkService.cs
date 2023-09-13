using AutoMapper;
using AutoMapper.QueryableExtensions;
using CryptoSystems.Data;
using CryptoSystems.Services.Interfaces;
using CryptoSystems.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace CryptoSystems.Services;

public class LaboratoryWorkService : ILaboratoryWorkService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public LaboratoryWorkService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<LaboratoryWorkViewModel> GetByIdAsync(int id)
    {
        var lab = await _context.LaboratoryWorks.FindAsync(id);

        var labViewModel = _mapper.Map<LaboratoryWorkViewModel>(lab);

        return labViewModel;
    }

    public async Task<IEnumerable<LaboratoryWorkListViewModel>> GetListAsync()
    {
        var labs = await _context.LaboratoryWorks
            .ProjectTo<LaboratoryWorkListViewModel>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return labs;
    }
}
