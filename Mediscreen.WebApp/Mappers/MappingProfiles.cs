using AutoMapper;
using Mediscreen.Shared.Entities;
using Mediscreen.WebApp.Models;

namespace Mediscreen.WebApp.Mappers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Patient, PatientViewModel>();
            CreateMap<PatientViewModel, Patient>();
            CreateMap<Note, NoteViewModel>();
            CreateMap<NoteViewModel, Note>();
        }
    }
}
