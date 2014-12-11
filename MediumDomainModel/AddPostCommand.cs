using MediatR;

namespace MediumDomainModel
{
    public class AddPostCommand : IRequest<string>
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public bool Published { get; set; }
    }
}