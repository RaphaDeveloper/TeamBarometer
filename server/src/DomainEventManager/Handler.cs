namespace DomainEventManager
{
	public abstract class Handler<TEvent> : IHandler
	{
		public abstract void Handle(TEvent domainEvent);

		public void Handle(object domainEvent)
		{
			Handle((TEvent)domainEvent);
		}
	}
}
