using MediatR;

namespace MediumDomainModel
{
    public class PostRequest : IRequest<PostModel>
    {
        public string Slug { get; set; }
    }
}