using AutoMapper;
using GoBlog.Areas.Admin.Models;
using GoBlog.Domain.Model;

namespace GoBlog
{
    public class AutoMapperConfig
    {
        public static void Init()
        {
            Mapper.CreateMap<PostInputModel, Post>();
        }         
    }
}