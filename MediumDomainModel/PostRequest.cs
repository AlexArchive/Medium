using MediatR;

namespace MediumDomainModel
{
    public class PostRequest : IRequest<PostModel>
    {
        public string PostSlug { get; set; }
    }
}