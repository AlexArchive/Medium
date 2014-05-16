using AutoMapper;
using Blog.Core.Infrastructure.Persistence.Entities;
using Blog.Infrastructure.AutoMapper.Profiles;
using Blog.Models;

namespace Blog.Infrastructure.AutoMapper
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.AddProfile(new EntryMapperProfile());
            Mapper.AddProfile(new PagedListMapperProfile());
            Mapper.AddProfile(new EntryInputMapperProfile());

            Mapper.CreateMap<BlogEntry, EntryInput>();
            Mapper.CreateMap<EntryInput, BlogEntry>();

        }
    }
}