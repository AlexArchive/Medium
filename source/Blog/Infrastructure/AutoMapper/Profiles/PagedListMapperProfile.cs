using System.Collections.Generic;
using AutoMapper;
using Blog.Core.Infrastructure.Persistence.Entities;
using Blog.Core.Paging;
using Blog.Models;

namespace Blog.Infrastructure.AutoMapper.Profiles
{
    public class PagedListMapperProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<PagedList<BlogEntry>, PagedList<Entry>>()
                .AfterMap((entity, model) => Mapper.Map<List<BlogEntry>, List<Entry>>(entity, model));
        }
    }
}