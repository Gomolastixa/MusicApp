using AutoMapper;
using MusicApp.Models;

namespace MusicApp.Configurations
{
    public class MapperConfig :Profile
    {
        public MapperConfig()
        {
            CreateMap<Musician, CreateMusicianDto>().ReverseMap();
            CreateMap<Musician, EditMusicianDto>().ReverseMap();

            CreateMap<MusicRecord, CreateMusicRecordDto>().ReverseMap();
            CreateMap<MusicRecord, EditMusicRecordDto>().ReverseMap();

        }
    }
}
