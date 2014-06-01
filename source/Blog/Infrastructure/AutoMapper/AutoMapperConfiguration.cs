using AutoMapper;
using Blog.Areas.Admin.Models;
using Blog.Domain.Infrastructure.Persistence.Entities;
using Blog.Domain.Paging;
using Blog.Infrastructure.Common;
using Blog.Models;
using MarkdownSharp;
using System.Collections.Generic;

namespace Blog.Infrastructure.AutoMapper
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
                    entry => SlugConverter.Convert(entry.Header)));

            Mapper.CreateMap<Entry, EntryInput>();
        }
    }
}