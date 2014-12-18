using MediatR;

namespace Medium.DomainModel
{
    public class AddPostCommand : IRequest<string>
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public string Tags { get; set; }
        public bool Published { get; set; }
    }
}