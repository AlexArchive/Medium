using MediatR;

namespace MediumDomainModel
{
    public class EditPostCommand : IRequest<string>
    {
        public string OriginalSlug { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public bool Published { get; set; }
    }
}