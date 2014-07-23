using AutoMapper;
using GoBlog.Areas.Admin.Models;
using GoBlog.Models;
using GoBlog.Persistence.Entities;

namespace GoBlog.Infrastructure.AutoMapper
{
    public class AutoMapperConfig
    {
        public static void Configure()
        {
            Mapper.CreateMap<Post, PostViewModel>();
         
            Mapper.CreateMap<Post, PostInputModel>()
                  .ForMember(post => post.Tags,
                             option => option.MapFrom(post => TagsResolver.ResolveFromCollection(post.Tags)));

            Mapper.CreateMap<PostInputModel, Post>()
                  .ForMember(post => post.Tags, 
                             option => option.MapFrom(post => TagsResolver.ResolveFromString(post.Tags)));
        }
    }
}