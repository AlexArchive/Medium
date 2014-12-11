using System.Collections.Generic;

namespace MediumDomainModel
{
    public class AllPostsRequest : MediatR.IRequest<IEnumerable<PostModel>>
    {
    }
}