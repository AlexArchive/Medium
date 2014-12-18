using System.Collections.Generic;
using Dapper;
using MediatR;

namespace Medium.DomainModel
{
    public class TagsRequestHandler : IRequestHandler<TagsRequest, IEnumerable<string>>
    {
        public IEnumerable<string> Handle(TagsRequest request)
        {
            using (var connection = SqlConnectionFactory.Create())
            {
                var param = new { Term = "%" + request.SearchTerm + "%"};

                return connection
                    .Query<string>("Select [Name] FROM [Tags] WHERE [Name] LIKE @Term", param);
            }
        }
    }
}