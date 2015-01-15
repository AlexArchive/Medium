using Dapper;
using MediatR;

namespace Medium.DomainModel
{
    public class UpdateConfigurationCommandHandler : IRequestHandler<UpdateConfigurationCommand, Unit>
    {
        public Unit Handle(UpdateConfigurationCommand command)
        {
            using (var connection = SqlConnectionFactory.Create())
            {
                connection.Execute("UPDATE [Configuration] SET [Value] = @BlogTitle WHERE [Key]='BlogTitle'", command.Configuration);
            }

            return Unit.Value;
        }
    }
}