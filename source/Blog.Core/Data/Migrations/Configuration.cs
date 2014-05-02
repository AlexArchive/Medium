using System;
using System.Data.Entity;
using System.Globalization;
using System.Threading;
using Blog.Core.Data.Entities;

namespace Blog.Core.Data.Migrations
{
    public class Configuration : DropCreateDatabaseIfModelChanges<BlogDatabase>
    {
        protected override void Seed(BlogDatabase context)
        {
            for (int i = 0; i < 15; i++)
            {
                var entry = new BlogEntry
                {
                    HeaderSlug = i.ToString(CultureInfo.InvariantCulture),
                    Header = "Test Post " + i.ToString(CultureInfo.InvariantCulture),
                    Content = "Craft beer bespoke photo booth Thundercats Wes Anderson. Street art kitsch kogi, Etsy Shoreditch Carles Vice occupy PBR&B Pitchfork twee cred cardigan 3 wolf moon. Kale chips PBR&B sartorial ethnic pickled. Wayfarers umami irony cliche roof party, mlkshk swag direct trade sartorial butcher viral literally PBR slow-carb. Fanny pack tattooed yr kale chips VHS, meh banh mi blog cray normcore wolf Bushwick fingerstache single-origin coffee quinoa. Fingerstache Thundercats bespoke organic roof party cred. Banh mi Cosby sweater Williamsburg, bitters try-hard fixie typewriter deep v ethical forage chia synth selvage cliche narwhal.",
                    PublishDate = DateTime.Now,
                    Summary = "Craft beer bespoke photo booth Thundercats Wes Anderson. Street art kitsch kogi, Etsy Shoreditch Carles Vice occupy PBR&B Pitchfork twee cred cardigan 3 wolf moon. Kale chips PBR&B sartorial ethnic pickled. Wayfarers umami irony cliche roof party, mlkshk swag direct trade sartorial butcher viral literally PBR slow-carb. Fanny pack tattooed yr kale chips VHS, meh banh mi blog cray normcore wolf Bushwick fingerstache single-origin coffee quinoa. Fingerstache Thundercats bespoke organic roof party cred. Banh mi Cosby sweater Williamsburg, bitters try-hard fixie typewriter deep v ethical forage chia synth selvage cliche narwhal.",
                    Views = 0
                };

                // Wait a little bit so that the PublishDate better reflects the order in which
                // the entries where inserted.
                Thread.Sleep(50);

                context.BlogEntries.Add(entry);

            }

            context.SaveChanges();

            base.Seed(context);
        }
    }
}
