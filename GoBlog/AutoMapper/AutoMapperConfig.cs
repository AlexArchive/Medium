using AutoMapper;
using GoBlog.Common;
using GoBlog.Data.Entities;
using GoBlog.Models;

namespace GoBlog.AutoMapper
{
    public class AutoMapperConfig
    {
        public static void Configure()
        {
            Mapper.CreateMap<Post, PostViewModel>();

            //Mapper.CreateMap<Post, PostInputModel>()
            //      .ForMember(post => post.Tags,
            //                 option => option.MapFrom(post => TagsResolver.ResolveFromCollection(post.Tags)));

            //Mapper.CreateMap<PostInputModel, Post>()
            //      .ForMember(post => post.Tags,
            //                 option => option.MapFrom(post => TagsResolver.ResolveFromString(post.Tags)));
        }
    }
}