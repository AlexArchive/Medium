using MediatR;

namespace Medium.DomainModel
{   
    public class UpdateConfigurationCommand : IRequest<Unit>
    {
        public ConfigurationModel Configuration { get; set; }
    }
}