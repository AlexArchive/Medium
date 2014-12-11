using System.Collections.Generic;
using MediatR;

namespace MediumDomainModel
{
    public class AllPostsRequest : IRequest<IEnumerable<PostModel>>
    {
    }
}