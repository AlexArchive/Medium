using System.Data;
using Dapper;
using MediatR;

namespace Medium.DomainModel
{
    public class UpdateConfigurationCommandHandler : IRequestHandler<UpdateConfigurationCommand, Unit>
    {
        private readonly IDbConnection _connection;

        public UpdateConfigurationCommandHandler(IDbConnection connection)
        {
            _connection = connection;
        }

        public Unit Handle(UpdateConfigurationCommand command)
        {
            _connection.Execute("UPDATE [Configuration] SET [Value] = @BlogTitle WHERE [Key]='BlogTitle'", command.Configuration);
            return Unit.Value;
        }
    }
}