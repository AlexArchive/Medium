using MediatR;

namespace Medium.DomainModel.Configuration
{   
    public class UpdateConfigurationCommand : IRequest<Unit>
    {
        public ConfigurationModel Configuration { get; set; }
    }
}