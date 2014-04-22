using System.Collections.Generic;
using System.Data.Entity;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using Blog.Core.Data.Entities;
using Blog.Core.Data.Migrations;
using Blog.Core.Paging;
using Blog.Models;
using Blog.Models.AdminModel;
using MarkdownSharp;

namespace Blog
{
    public class MvcApplication : HttpApplication
    {
        private readonly Markdown _markdown = new Markdown();

        protected void Application_Start()
        {
            Mapper.CreateMap<BlogEntry, EntryViewModel>()
                .ForMember(entry => entry.Content,
                    expression => expression.ResolveUsing(source => _markdown.Transform(source.Content)));

            Mapper.CreateMap<PagedList<BlogEntry>, PagedList<EntryViewModel>>()
                .AfterMap((source, destination) => Mapper.Map<List<BlogEntry>, List<EntryViewModel>>(source, destination));

            Mapper.AssertConfigurationIsValid();


            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //HibernatingRhinos.Profiler.Appender.EntityFramework.EntityFrameworkProfiler.Initialize();
            Database.SetInitializer(new Configuration());
        }
    }


}