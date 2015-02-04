using System.Collections.Generic;
using System.Data;
using Dapper;
using MediatR;

namespace Medium.DomainModel
{
    public class TagSequenceRequestHandler : IRequestHandler<TagSequenceRequest, IEnumerable<string>>
    {
        private readonly IDbConnection _connection;

        public TagSequenceRequestHandler(IDbConnection connection)
        {
            _connection = connection;
        }

        public IEnumerable<string> Handle(TagSequenceRequest request)
        {
            var param = new { Term = "%" + request.SearchTerm + "%" };

            return _connection
                .Query<string>("Select Name FROM dbo.Tags WHERE Name LIKE @Term", param);
        }
    }
}