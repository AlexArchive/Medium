using System.Collections.Generic;
using MediatR;

namespace MediumDomainModel
{
    public class AllPostsRequest : IRequest<IEnumerable<PostModel>>
    {
        public bool IncludeDrafts { get; set; }

        public AllPostsRequest(bool includeDrafts)
        {
            IncludeDrafts = includeDrafts;
        }
    }
}