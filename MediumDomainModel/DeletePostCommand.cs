using MediatR;

namespace MediumDomainModel
{
    public class DeletePostCommand : IRequest
    {
        public string PostSlug { get; set; }
    }
}