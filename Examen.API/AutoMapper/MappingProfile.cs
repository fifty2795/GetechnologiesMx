using AutoMapper;
using Examen.API.Business.DTO;
using Examen.API.Data.Models;
using Examen.API.Models;

namespace Examen.API.AutoMapper
{
    public class MappingProfile: Profile
    {
        public MappingProfile() 
        {
            // Login
            CreateMap<LoginViewModel, LoginDTO>();

            // Persona
            CreateMap<TblPersona, PersonaDTO>()
                  .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                  .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre))
                  .ForMember(dest => dest.ApellidoPaterno, opt => opt.MapFrom(src => src.ApellidoPaterno))
                  .ForMember(dest => dest.ApellidoMaterno, opt => opt.MapFrom(src => src.ApellidoMaterno))
                  .ForMember(dest => dest.Identificacion, opt => opt.MapFrom(src => src.Identificacion));

            CreateMap<PersonaViewModel, TblPersona>();

            // Factura            
            CreateMap<TblFactura, FacturaDTO>()
                .ForMember(dest => dest.Persona, opt => opt.MapFrom(src => src.IdPersonaNavigation));
            
            CreateMap<FacturaDTO, TblFactura>()
                .ForMember(dest => dest.IdPersonaNavigation, opt => opt.Ignore());

            CreateMap<FacturaViewModel, TblFactura>()
                .ForMember(dest => dest.IdPersona, opt => opt.MapFrom(src => src.IdPersona)) 
                .ForMember(dest => dest.Monto, opt => opt.MapFrom(src => src.Monto));                  
        }
    }
}
