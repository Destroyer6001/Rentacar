using AutoMapper;
using Rentacar.Models;

namespace Rentacar.Servicios
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<VehiculoViewModel, VehiculoCreacionViewModel>();
        }
    }
}
