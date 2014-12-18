using System.Collections.Generic;
using MediatR;

namespace Medium.DomainModel
{
    public class TagsRequest : IRequest<IEnumerable<string>>
    {
        public string SearchTerm { get; set; }
    }
}