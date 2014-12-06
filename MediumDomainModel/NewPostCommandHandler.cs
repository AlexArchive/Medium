using Dapper;
using Medium.WebModel;

namespace MediumDomainModel
{
    public class NewPostCommand
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public bool Published { get; set; }
    }

    public class NewPostCommandHandler : ICommandHandler<NewPostCommand, string>
    {
        public string Handle(NewPostCommand command)
        {
            using (var connection = Database.Connect())
            {
                var o = new
                {
                    Slug = command.Title.ToSlug(),
                    command.Title,
                    command.Body,
                    command.Published
                };
                connection.Execute(
                    @"INSERT INTO [Posts] 
                      VALUES (@Slug, @Title, @Body, @Published, GETDATE())", o
                    );

                return o.Slug;
            }
        }
    }
}