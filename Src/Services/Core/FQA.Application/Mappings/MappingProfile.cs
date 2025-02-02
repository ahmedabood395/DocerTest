namespace FAQ.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PutResolveReportCommand, ResolveReport>();

        }
    }
}