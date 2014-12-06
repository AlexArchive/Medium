using Dapper;

namespace MediumDomainModel
{
    public class NewPostHandler
    {
        public void Handle(PostModel post)
        {
            using (var connection = Database.Connect())
            {
                connection.Execute(
                    @"INSERT INTO [Posts] 
                      VALUES (@Slug, @Title, @Body, @Published, GETDATE())", 
                    post);
            }
        }
    }
}