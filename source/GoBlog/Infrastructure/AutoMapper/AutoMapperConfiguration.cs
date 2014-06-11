using System.Collections.Generic;
using AutoMapper;
using GoBlog.Areas.Admin.Models;
using GoBlog.Domain.Infrastructure.Persistence.Entities;
using GoBlog.Domain.Paging;
using GoBlog.Infrastructure.AutoMapper.Resolvers;
using GoBlog.Infrastructure.Common;
using GoBlog.Models;
using MarkdownSharp;

namespace GoBlog.Infrastructure.AutoMapper
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            var options = new MarkdownOptions { AutoHyperlink = true };
            var converter = new Markdown(options);

            Mapper.CreateMap<Entry, EntryViewModel>()
                // convert content from markdown to html
                .ForMember(entry => entry.Content,
                    expression => expression.ResolveUsing(source => converter.Transform(source.Content)))
                // convert summary from markdown to html
                .ForMember(entry => entry.Summary,
                    expression => expression.ResolveUsing(source => converter.Transform(source.Summary)));

            // convert the closed type of the derived generic list
            Mapper.CreateMap<PagedList<Entry>, PagedList<EntryViewModel>>()
                .AfterMap((entity, model) => Mapper.Map<List<Entry>, List<EntryViewModel>>(entity, model));

            Mapper.CreateMap<EntryInput, Entry>()
                .ForMember(entry => entry.Slug, expression => expression.MapFrom(
                    entry => SlugConverter.Convert(entry.Header)))
                .ForMember(entry => entry.Tags, expression => expression.MapFrom(
                    entry => TagsResolver.ResolveTags(entry.Tags)));

            Mapper.CreateMap<Entry, EntryInput>()
                .ForMember(entry => entry.Tags, opt => opt.MapFrom(
                    entry => TagsResolver.ResolveTags(entry.Tags)));

            Mapper.CreateMap<Entry, EntryInput>();
        }
    }
}