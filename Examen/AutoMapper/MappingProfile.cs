using AutoMapper;
using Examen.Business.DTO;
using Examen.Models;

namespace Examen.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Mapear Persona
            CreateMap<PersonaViewModel, PersonaDto>().ReverseMap();

            // Mapear Factura
            CreateMap<FacturaViewModel, FacturaDTO>().ReverseMap();
            CreateMap<Examen.Models.PersonaDTO, Examen.Business.DTO.PersonaDTO>().ReverseMap();
        }
    }
}
