using System.Collections.Generic;
using Dapper;
using MediatR;

namespace Medium.DomainModel
{
    public class TagSequenceRequestHandler : IRequestHandler<TagSequenceRequest, IEnumerable<string>>
    {
        public IEnumerable<string> Handle(TagSequenceRequest request)
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