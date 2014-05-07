using AutoMapper;
using Blog.Infrastructure.AutoMapper.Profiles;

namespace Blog.Infrastructure.AutoMapper
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.AddProfile(new EntryMapperProfile());
            Mapper.AddProfile(new PagedListMapperProfile());
        }
    }
}