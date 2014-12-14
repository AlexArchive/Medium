using System.Collections.Generic;
using MediatR;

namespace Medium.DomainModel
{
    public class AllPostsRequest : IRequest<IEnumerable<PostModel>>
    {
        public bool IncludeDrafts { get; set; }
    }
}