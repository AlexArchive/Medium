using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Core.Data.Entities;

namespace Blog.Core.Data.Migrations
{
    public class Configuration : DropCreateDatabaseAlways<BlogDatabase>
    {
        protected override void Seed(BlogDatabase context)
        {
            for (int i = 0; i < 15; i++)
            {
                var entry = new BlogEntry
                {
                    HeaderSlug = i.ToString(CultureInfo.InvariantCulture),
                    Header = "Test Post",
                    Content = "Craft beer bespoke photo booth Thundercats Wes Anderson. Street art kitsch kogi, Etsy Shoreditch Carles Vice occupy PBR&B Pitchfork twee cred cardigan 3 wolf moon. Kale chips PBR&B sartorial ethnic pickled. Wayfarers umami irony cliche roof party, mlkshk swag direct trade sartorial butcher viral literally PBR slow-carb. Fanny pack tattooed yr kale chips VHS, meh banh mi blog cray normcore wolf Bushwick fingerstache single-origin coffee quinoa. Fingerstache Thundercats bespoke organic roof party cred. Banh mi Cosby sweater Williamsburg, bitters try-hard fixie typewriter deep v ethical forage chia synth selvage cliche narwhal.",
                    PublishDate = DateTime.Now,
                    Published = true,
                    Summary = "Craft beer bespoke photo booth Thundercats Wes Anderson. Street art kitsch kogi, Etsy Shoreditch Carles Vice occupy PBR&B Pitchfork twee cred cardigan 3 wolf moon. Kale chips PBR&B sartorial ethnic pickled. Wayfarers umami irony cliche roof party, mlkshk swag direct trade sartorial butcher viral literally PBR slow-carb. Fanny pack tattooed yr kale chips VHS, meh banh mi blog cray normcore wolf Bushwick fingerstache single-origin coffee quinoa. Fingerstache Thundercats bespoke organic roof party cred. Banh mi Cosby sweater Williamsburg, bitters try-hard fixie typewriter deep v ethical forage chia synth selvage cliche narwhal.",
                    Views = 0
                };

                context.BlogEntries.Add(entry);

            }

            context.SaveChanges();

            base.Seed(context);
        }
    }
}
