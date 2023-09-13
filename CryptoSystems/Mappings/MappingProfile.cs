using AutoMapper;
using CryptoSystems.Models;
using CryptoSystems.ViewModels;

namespace CryptoSystems.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<LaboratoryWork, LaboratoryWorkListViewModel>();
        CreateMap<LaboratoryWork, LaboratoryWorkViewModel>();
    }
}
