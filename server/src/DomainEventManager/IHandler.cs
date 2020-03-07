namespace DomainEventManager
{
	public interface IHandler
	{
		void Handle(object domainEvent);
	}
}
