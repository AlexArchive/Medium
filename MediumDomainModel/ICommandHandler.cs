namespace MediumDomainModel
{
    public interface ICommandHandler<in TCommand>
    {
        void Handle(TCommand command);
    }

    public interface ICommandHandler<in TCommand, out TResult>
    {
        TResult Handle(TCommand command);
    }
}