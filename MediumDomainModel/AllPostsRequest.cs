using System.Collections.Generic;
using MediatR;

namespace Medium.DomainModel
{
    public class AllPostsRequest : IRequest<IEnumerable<PostModel>>
    {
         
    }
}