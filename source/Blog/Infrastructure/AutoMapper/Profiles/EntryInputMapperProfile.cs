using System;
using AutoMapper;
using Blog.Core.Infrastructure.Persistence.Entities;
using Blog.Infrastructure.Common;
using Blog.Models;

namespace Blog.Infrastructure.AutoMapper.Profiles
{
    public class EntryInputMapperProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<EntryInput, BlogEntry>()
                .ForMember(entry => entry.HeaderSlug, expression => expression.MapFrom(entry => SlugConverter.Convert(entry.Header)))
                .ForMember(entry => entry.PublishDate, expression => expression.MapFrom(entry => DateTime.Now))
                .ForMember(entry => entry.Summary, expression => expression.MapFrom(entry => MakeSummary(entry.Content)));
        }

        private static string MakeSummary(string content)
        {
            return content.Length < 750 ? content : content.Substring(0, 750) + "...";
        }
    }
}