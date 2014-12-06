namespace MediumDomainModel
{
    public interface ICommandHandler<in TCommand>
    {
        void Handle(TCommand command);
    }
}