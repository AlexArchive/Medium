using MediatR;

namespace Medium.DomainModel
{
    public class PostRequest : IRequest<PostModel>
    {
        public string PostSlug { get; set; }
    }
}