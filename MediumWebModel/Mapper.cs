using System;

namespace Medium.WebModel
{
    public static class Mapper
    {
        public static TDestination MapTo<TDestination>(this object source)
        {
            var instance = Activator.CreateInstance<TDestination>();

            foreach (var sourceProp in source.GetType().GetProperties())
            {
                var destinationProp = instance
                    .GetType()
                    .GetProperty(sourceProp.Name);

                if (destinationProp != null)
                {
                    destinationProp.SetValue(
                        instance, 
                        sourceProp.GetValue(source));
                }
            }

            return instance;
        }
    }
}