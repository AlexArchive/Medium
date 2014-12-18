using MediatR;

namespace Medium.DomainModel
{
    public class EditPostCommand : IRequest<string>
    {
        public string OriginalSlug { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string Tags { get; set; }
        public bool Published { get; set; }
    }
}