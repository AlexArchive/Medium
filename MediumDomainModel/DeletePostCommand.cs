using MediatR;

namespace Medium.DomainModel
{
    public class DeletePostCommand : IRequest
    {
        public string PostSlug { get; set; }
    }
}