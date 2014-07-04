using System.Collections.Generic;
using AutoMapper;
using GoBlog.Infrastructure.Persistence.Entities;
using GoBlog.Models;

namespace GoBlog.Infrastructure.AutoMapper
{
    public class AutoMapperConfig
    {
        public static void Configure()
        {
            Mapper.CreateMap<Post, PostViewModel>();
            Mapper.CreateMap<IEnumerable<Post>, IEnumerable<PostViewModel>>();
        }
    }
}