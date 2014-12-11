using MediatR;

namespace MediumDomainModel
{
    public class DeletePostCommand : IRequest
    {
        public string Slug { get; set; }
    }
}