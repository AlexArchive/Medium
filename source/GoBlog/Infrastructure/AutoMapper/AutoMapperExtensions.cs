using System;
using AutoMapper;

namespace GoBlog.Infrastructure.AutoMapper
{
    public static class AutoMapperExtensions
    {
        public static TResult MapPropertiesToInstance<TResult>(this object self, TResult value)
        {
            if (self == null)
                throw new ArgumentNullException();

            return (TResult) Mapper.Map(self, value, self.GetType(), typeof(TResult));
        }
    }
}