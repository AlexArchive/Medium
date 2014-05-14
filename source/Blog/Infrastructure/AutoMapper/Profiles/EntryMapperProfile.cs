using AutoMapper;
using Blog.Core.Infrastructure.Persistence.Entities;
using Blog.Models;
using MarkdownSharp;

namespace Blog.Infrastructure.AutoMapper.Profiles
{
    public class EntryMapperProfile : Profile
    {
        private readonly Markdown _markdown; 

        public EntryMapperProfile()
        {
            _markdown = new Markdown(new MarkdownOptions { AutoHyperlink = true });
        }

        protected override void Configure()
        {
            Mapper.CreateMap<BlogEntry, Entry>()
                // convert content from markdown to html
                .ForMember(entry => entry.Content,
                    expression => expression.ResolveUsing(source => _markdown.Transform(source.Content)))
                // convert summary from markdown to html
                .ForMember(entry => entry.Summary,
                    expression => expression.ResolveUsing(source => _markdown.Transform(source.Summary)));
        }
    }
}