using MediatR;

namespace Medium.DomainModel
{
    public class DeletePostCommand : IRequest<bool>
    {
        public string PostSlug { get; set; }
    }
}